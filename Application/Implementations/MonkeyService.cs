using Application.Contracts;
using CSharpFunctionalExtensions;
using Domain.Entities;
using Domain.Models;
using Infrastructure.Contracts;
using Application.Extensions;

namespace Application.Implementations
{
    public class MonkeyService : IMonkeyService
    {
        private readonly IAdmissionTracker _admissionTracker;
        private readonly IMonkeyRepository _monkeyRepository;
        private readonly IDepartureService _departureService;

        public MonkeyService(IAdmissionTracker admissionTracker, IMonkeyRepository monkeyRepository, IDepartureService departureService)
        {
            _admissionTracker = admissionTracker;
            _monkeyRepository = monkeyRepository;
            _departureService = departureService;
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

            var departureResult = await _departureService.Depart(request.Value.MonkeyId);

            if (departureResult.IsFailure)
            {
                return Result.Failure("Failed writing the departure data");
            }

            return Result.Success();
           
        }

        public async Task<List<MonkeyReportResponse>> GetMonkeyBySpecies(Maybe<MonkeySpecies> species)
        {
            return await _monkeyRepository.GetMonkeysBySpecies(species.Value);
        }

        public async Task<List<MonkeyReportResponse>> GetMonkeysByDate(DateTime dateFrom, DateTime dateTo)
        {
            return await _monkeyRepository.GetMonkeysByDate(dateFrom, dateTo);
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
    }
}
