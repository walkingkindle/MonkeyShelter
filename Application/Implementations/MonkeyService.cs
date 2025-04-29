using Application.Contracts;
using CSharpFunctionalExtensions;
using Domain.Entities;
using Domain.Models;
using Infrastructure.Contracts;
using Application.Extensions;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Implementations
{
    public class MonkeyService : IMonkeyService
    {
        private readonly IAdmissionTracker _admissionTracker;
        private readonly IMonkeyRepository _monkeyRepository;
        private readonly IDepartureService _departureService;
        private readonly IMemoryCache _memoryCache;

        public MonkeyService(IAdmissionTracker admissionTracker, IMonkeyRepository monkeyRepository, IDepartureService departureService, IMemoryCache memoryCache)
        {
            _admissionTracker = admissionTracker;
            _monkeyRepository = monkeyRepository;
            _departureService = departureService;
            _memoryCache = memoryCache;
        }
        public async Task<Result> AddMonkey(Maybe<MonkeyEntryRequest> request)
        {
            var requestResult = request.ToResult("Request for entry cannot be null")
                .Ensure(_ => _admissionTracker.CanMonkeyBeAdmitted(), "Shelter is currently full");

            if (requestResult.IsFailure)
            {
                return Result.Failure(requestResult.Error);
            }

            var createResult = Monkey.CreateMonkey(request);

            if (createResult.IsFailure)
            {
                return Result.Failure(createResult.Error);
            }

            var monkeyIdResult = await _monkeyRepository.AddMonkeyToShelter(createResult.Value);

            if (monkeyIdResult.IsFailure)
            {
                return Result.Failure(monkeyIdResult.Error);
            }

            InvalidateCacheForSpecies(createResult.Value.Species);

            var admittanceResult = await _admissionTracker.Admit(monkeyIdResult.AsMaybe());

            if (admittanceResult.IsFailure)
            {
                return Result.Failure(admittanceResult.Error);
            }

            return Result.Success();

        }
         
        public async Task<Result> DepartMonkey(Maybe<MonkeyDepartureRequest> request)
        {
            var idResult = request.ToResult("monkey Id must not be null");

            if (idResult.IsFailure)
            {
                return Result.Failure(idResult.Error);
            }

            var monkeyResult = await _monkeyRepository.GetMonkeyById(idResult.Value.MonkeyId);

            if (monkeyResult.IsFailure)
            {
                return Result.Failure(monkeyResult.Error);
            }

            var departurePermission = _departureService.CanMonkeyDepart(monkeyResult.Value.Species);

            if (!departurePermission)
            {
                return Result.Failure("We cannot depart the monkey at this time.");
            }

            await _monkeyRepository.RemoveMonkeyFromShelter(monkeyResult.Value);

            InvalidateCacheForSpecies(monkeyResult.Value.Species);

            var departureResult = await _departureService.Depart(request.Value.MonkeyId);

            if (departureResult.IsFailure)
            {
                return Result.Failure("Failed writing the departure data");
            }

            return Result.Success();
           
        }

        public async Task<Result<List<MonkeyReportResponse>>> GetMonkeyBySpecies(Maybe<MonkeySpecies> species)
        {
            var validationResult = species.ToResult("Species cannot be null")
                .Ensure(species => Enum.IsDefined(typeof(MonkeySpecies), species), "Invalid species value");

            if (validationResult.IsFailure)
            {
                return Result.Failure<List<MonkeyReportResponse>>(validationResult.Error);
            }

            var cacheKey = $"MonkeySpecies_{species.Value}";
            if (_memoryCache.TryGetValue(cacheKey, out List<MonkeyReportResponse> cachedResult))
            {
                return Result.Success(cachedResult);
            }

            var result = await _monkeyRepository.GetMonkeysBySpecies(species.Value);

            if (result.Any())
            {
                _memoryCache.Set(cacheKey, result, TimeSpan.FromMinutes(10)); // Cache expires after 10 minutes (adjust as needed)
            }

            return Result.Success(result);
        }

        public async Task<Result<List<MonkeyReportResponse>>> GetMonkeysByDate(MonkeyDateRequest dateTimes)
        {
            var validationResult = dateTimes.ToResult("DateTimesCannot be null")
            .Ensure(result => !(result.DateFrom.Value > result.DateTo.Value), "Date from must not be higher than date to");

            if (validationResult.IsFailure)
            {
                return Result.Failure<List<MonkeyReportResponse>>("Date times provided are invalid");
            }
            var result = await _monkeyRepository.GetMonkeysByDate(dateTimes.DateFrom.Value, dateTimes.DateTo.Value);

            return Result.Success(result);
        }

        public async Task<Result> UpdateMonkeyWeight(Maybe<MonkeyWeightRequest> request)
        {
            var requestResult = request.ToResult("Monkey must have some weight present")
                .Ensure(request => request.MonkeyId >= 0, "monkey Id must be valid")
                .Ensure(request => request.NewMonkeyWeight > 0, "Monkey must have a valid weight");

            if (requestResult.IsFailure)
            {
                return Result.Failure(requestResult.Error);
            }

            await _monkeyRepository.UpdateMonkey(request.Value);

            return Result.Success();
        }

        private void InvalidateCacheForSpecies(MonkeySpecies species)
        {
            var cacheKey = $"MonkeySpecies_{species}";
            _memoryCache.Remove(cacheKey);
        }


    }
}
