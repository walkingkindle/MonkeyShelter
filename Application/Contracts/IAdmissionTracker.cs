using CSharpFunctionalExtensions;

namespace Application.Contracts
{
    public interface IAdmissionTracker
    {
        public Result CanMonkeyBeAdmitted();
        public Task<int> GetAdmissionsForToday();

        public Task IncrementAdmissions();
    }
}
