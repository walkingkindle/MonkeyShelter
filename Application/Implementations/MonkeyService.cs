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
 
        public Result AdjustMonkeyWeight()
        {
            throw new NotImplementedException();
        }

        public Task<Result> DepartMonkey(Maybe<MonkeyDepartureRequest> request)
        {
            return request.ToResult("monkey Id must not be null")
           .Ensure(result => _admissionTracker.IsSufficientMonkeyDeparture(), "Cannot be departed at this time")
           .Bind(async result => await _monkeyRepository.GetMonkeyById(request.Value.MonkeyId)
           .Ensure(result => MonkeyCanBeDeparted(result.Species), "We could not depart the selected monkey at this time.")
           .OnSuccessTry(async result => await _monkeyRepository.RemoveMonkeyFromShelter(result)));
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
