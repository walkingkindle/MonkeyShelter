using CSharpFunctionalExtensions;
using Domain.Entities;

namespace Application.Contracts
{
    public interface IAdmissionTracker
    {
        public bool CanMonkeyBeAdmitted();
        public Task<int> GetAdmissionsForToday();

        public Task<Result> IncrementAdmissions(int monkeyId);

        public bool CanMonkeyDepart(MonkeySpecies species);

        public bool IsSufficientMonkeyDeparture();


    }
}
