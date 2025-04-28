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
        public IAdmissionTracker _admissionTracker;
        public IMonkeyRepository _monkeyRepository;

        public MonkeyService(IAdmissionTracker admissionTracker, IMonkeyRepository monkeyRepository)
        {
            _admissionTracker = admissionTracker;
            _monkeyRepository = monkeyRepository;
        }

        public async Task<Result> AddMonkey(Maybe<MonkeyEntryRequest> request)
        {
            return request.ToResult("Request for entry cannot be null")
            .Ensure(result => MonkeyCanBeAdmitted(), "Shelter is currently full")
            .Map(monkey => Monkey.CreateMonkey(request)
            .OnSuccess(async monkey => await _monkeyRepository.AddMonkeyToShelter(monkey))
            .OnSuccess(async monkey => await _admissionTracker.IncrementAdmissions(monkey.Id)));
        }
 
        public Task<Result> DepartMonkey(Maybe<MonkeyDepartureRequest> request)
        {
            return request.ToResult("monkey Id must not be null")
           .Ensure(result => _admissionTracker.IsSufficientMonkeyDeparture(), "Cannot be departed at this time")
           .Bind(async result => await _monkeyRepository.GetMonkeyById(request.Value.MonkeyId)
           .EnsureNotNull("We could not find the specified monkeyId")
           .Ensure(result => MonkeyCanBeDeparted(result.Species), "We could not depart the selected monkey at this time.")
           .OnSuccessTry(async result => await _monkeyRepository.RemoveMonkeyFromShelter(result)));
        }

        public async Task<List<MonkeyReportResponse>> GetMonkeyBySpecies(Maybe<MonkeySpecies> species)
        {
            return await _monkeyRepository.GetMonkeysBySpecies(species.Value);
        }

        public async Task<List<MonkeyReportResponse>> GetMonkeysByDate(DateTime dateFrom, DateTime dateTo)
        {
            return await _monkeyRepository.GetMonkeysByDate(dateFrom, dateTo);
        }

        public Task<Result> UpdateMonkeyWeight(Maybe<MonkeyWeightRequest> request)
        {
                 return request.ToResult("Monkey must have some weight present")
                .Ensure(request => request.MonkeyId >= 0, "monkey Id must be valid")
                .Ensure(request => request.NewMonkeyWeight > 0, "Monkey must have a valid weight")
                .Bind(async result => await _monkeyRepository.GetMonkeyById(request.Value.MonkeyId))
                .EnsureNotNull("We could not find the specified monkeyId")
                .OnSuccessTry(async result => await _monkeyRepository.UpdateMonkey(request.Value));
        }

        private bool MonkeyCanBeAdmitted()
        {
           return _admissionTracker.CanMonkeyBeAdmitted();
        }

        private bool MonkeyCanBeDeparted(MonkeySpecies species)
        {
           return _admissionTracker.CanMonkeyDepart(species);
        }
    }
}
