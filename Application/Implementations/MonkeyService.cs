using Application.Contracts;
using CSharpFunctionalExtensions;
using Domain.Entities;
using Domain.Models;
using Infrastructure.Contracts;

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
            Result admissionResult = _admissionTracker.CanMonkeyBeAdmitted();

            Result<Monkey> monkeyResult = Monkey.CreateMonkey(request);

            var combinedResult = Result.Combine(admissionResult, monkeyResult);

            if (combinedResult.IsFailure)
            {
                return Result.Failure(combinedResult.Error);
            }
            else
            {
                await _monkeyRepository.AddMonkeyToShelter(monkeyResult.Value);

                await _admissionTracker.IncrementAdmissions();

                return Result.Success();
            }
        }
 
        public Result AdjustMonkeyWeight()
        {
            throw new NotImplementedException();
        }

        public Result DepartMonkey()
        {
            throw new NotImplementedException();
        }
    }
}
