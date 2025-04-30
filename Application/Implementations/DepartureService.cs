using Application.Contracts;
using Application.Contracts.Repositories;
using CSharpFunctionalExtensions;
using Domain.Enums;

namespace Application.Implementations
{
    public class DepartureService : IDepartureService
    {

        private readonly IDeparturesRepository _departuresRepository;
        private readonly IAdmissionsRepository _admissionsRepository;

        public DepartureService(IDeparturesRepository departuresRepository, IAdmissionsRepository admissionsRepository)
        {
            _departuresRepository = departuresRepository;
            _admissionsRepository = admissionsRepository;
        }

        public bool CanMonkeyDepart(MonkeySpecies species)
        {
            
            return _departuresRepository.GetTodayDeparturesAmount() < 6
                && _admissionsRepository.GetMonkeysAmountBySpecies(species) >= 1
                && _admissionsRepository.GetTodayAdmittanceAmount() <= 2;
        }

        public async Task<Result> Depart(Maybe<int> monkeyId)
        {
            return await monkeyId.ToResult("monkey id must not be null")
                 .Ensure(monkeyId => monkeyId >= 0, "Monkey Id must be valid")
                 .OnSuccessTry(async result => await _departuresRepository.Depart(monkeyId.Value));

        }
    }
}
