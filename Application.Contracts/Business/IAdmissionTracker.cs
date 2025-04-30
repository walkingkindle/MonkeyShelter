using CSharpFunctionalExtensions;

namespace Application.Contracts.Business
{
    public interface IAdmissionTracker
    {
        public Task<Result> Admit(int monkeyId);
    }
}
