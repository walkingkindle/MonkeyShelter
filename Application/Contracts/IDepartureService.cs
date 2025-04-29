using CSharpFunctionalExtensions;
using Domain.Entities;

namespace Application.Contracts
{
    public interface IDepartureService
    {
        public bool CanMonkeyDepart(MonkeySpecies species);

        public Task<Result> Depart(Maybe<int> monkeyId);
    }
}
