using CSharpFunctionalExtensions;
using Domain.Enums;

namespace Application.Contracts
{
    public interface IDepartureService
    {
        public bool CanMonkeyDepart(MonkeySpecies species);

        public Task<Result> Depart(Maybe<int> monkeyId);
    }
}
