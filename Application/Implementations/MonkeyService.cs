using Application.Contracts;
using CSharpFunctionalExtensions;
using Domain.Entities;
using Application.Extensions;
using Microsoft.Extensions.Caching.Memory;
using Domain.Enums;
using Application.Contracts.Repositories;
using Application.Shared.Models;
using Domain.DatabaseModels;
using Application.Contracts.Business;

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
        public async Task<Result<MonkeyReportResponse>> AddMonkey(Maybe<MonkeyEntryRequest> request, int shelterId)
        {
            var requestResult = request.ToResult("Request for entry cannot be null");

            if (requestResult.IsFailure)
            {
                return Result.Failure<MonkeyReportResponse>(requestResult.Error);
            }

            var requestValue = request.Value;
            var createResult = Monkey.Create(
                monkeyName:requestValue.Name,
                monkeyWeight:requestValue.Weight,
                monkeySpecies:requestValue.Species,
                shelterId:shelterId
                );

            if (createResult.IsFailure)
            {
                return Result.Failure<MonkeyReportResponse>(createResult.Error);
            }

            MonkeyDbModel monkeyDbModel = new MonkeyDbModel(species:createResult.Value.Species,
                name:createResult.Value.Name,
                weight:createResult.Value.Weight,
                lastUpdateTime:null,
                shelterId:createResult.Value.ShelterId);
            var monkeyIdResult = await _monkeyRepository.AddMonkeyToShelter(monkeyDbModel);

            if (monkeyIdResult.IsFailure)
            {
                return Result.Failure<MonkeyReportResponse>(monkeyIdResult.Error);
            }

            InvalidateCacheForSpecies(createResult.Value.Species);

            var admittanceResult = await _admissionTracker.Admit(monkeyIdResult.Value);

            if (admittanceResult.IsFailure)
            {
                return Result.Failure<MonkeyReportResponse>(admittanceResult.Error);
            }

            return Result.Success(new MonkeyReportResponse
            {
                Id = monkeyIdResult.Value,
                Name = createResult.Value.Name,
                Weight = createResult.Value.Weight,
                Species = createResult.Value.Species,
            });

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

            var departureResult = await _departureService.Depart(request.Value.MonkeyId);

            if (departureResult.IsFailure)
            {
                return Result.Failure("Failed writing the departure data");
            }

            await _monkeyRepository.RemoveMonkeyFromShelter(monkeyResult.Value);

            InvalidateCacheForSpecies(monkeyResult.Value.Species);



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

            var validSpecies = validationResult.Value;
            var cacheKey = $"MonkeySpecies_{validSpecies}";

            if (_memoryCache.TryGetValue(cacheKey, out List<MonkeyReportResponse>? cachedResult))
            {
                return Result.Success(cachedResult ?? new List<MonkeyReportResponse>());
            }
            var result = await _monkeyRepository.GetMonkeysBySpecies(validSpecies);

            if (result != null && result.Any())
            {
                _memoryCache.Set(cacheKey, result, TimeSpan.FromMinutes(10));
            }

            return Result.Success(result);
        }

        public async Task<Result<List<MonkeyReportResponse>>> GetMonkeysByDate(MonkeyDateRequest dateTimes)
        {
            var validationResult = dateTimes.ToResult("DateTimesCannot be null")
            .Ensure(result => !(result.DateFrom > result.DateTo), "Date from must not be higher than date to");

            if (validationResult.IsFailure)
            {
                return Result.Failure<List<MonkeyReportResponse>>("Date times provided are invalid");
            }
            var result = await _monkeyRepository.GetMonkeysByDate(dateTimes.DateFrom, dateTimes.DateTo);

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
