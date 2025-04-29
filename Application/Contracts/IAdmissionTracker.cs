using CSharpFunctionalExtensions;
using Domain.Entities;

namespace Application.Contracts
{
    public interface IAdmissionTracker
    {
        public bool CanMonkeyBeAdmitted();
        public Task<Result> Admit(Maybe<int> monkeyId);
    }
}
