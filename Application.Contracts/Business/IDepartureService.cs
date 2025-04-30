using CSharpFunctionalExtensions;
using Domain.Enums;

namespace Application.Contracts.Business
{
    public interface IDepartureService
    {
        public bool CanMonkeyDepart(MonkeySpecies species);

        public Task<Result> Depart(int monkeyId);
    }
}
