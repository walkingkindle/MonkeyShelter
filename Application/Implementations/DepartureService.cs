using Application.Contracts;
using Application.Contracts.Business;
using Application.Contracts.Repositories;
using CSharpFunctionalExtensions;
using Domain.Entities;
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

        public async Task<Result> Depart(int monkeyId)
        {
            var departureResult = Departure.Create(monkeyId);

            if (departureResult.IsFailure)
                return Result.Failure(departureResult.Error);

            await _departuresRepository.Depart(monkeyId);

            return Result.Success();

        }
    }
}
