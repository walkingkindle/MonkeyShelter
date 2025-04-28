using Application.Contracts;
using CSharpFunctionalExtensions;
using Domain.Entities;
using Domain.Models;
using Infrastructure.Contracts;
using System.Threading.Tasks;

namespace Application.Implementations
{
    public class AdmissionsTracker : IAdmissionTracker
    {
        private readonly IAdmissionsRepository _admissionsRepository;

        private readonly IDeparturesRepository _departuresRepository;

        private const int maxNumberOfAdmittance = 7;

        private const int maxNumberOfDepartures = 5;

        public AdmissionsTracker(IAdmissionsRepository admissionsRepository, IDeparturesRepository departuresRepository)
        {
            _admissionsRepository = admissionsRepository;
            _departuresRepository = departuresRepository;
        }

        public bool CanMonkeyBeAdmitted()
        {
            return _admissionsRepository.GetTodayAdmittanceAmount() > maxNumberOfAdmittance;
        }
        public bool CanMonkeyDepart(MonkeySpecies species)
        {
            return _departuresRepository.GetTodayDeparturesAmount() < 6 && _admissionsRepository.GetMonkeysAmountBySpecies(species) >= 1;
        }

        public bool IsSufficientMonkeyDeparture()
        {
            return _admissionsRepository.GetTodayAdmittanceAmount() <= 2;
        }

        public Task<int> GetAdmissionsForToday()
        {
            throw new NotImplementedException();
        }

        public async Task<Result> IncrementAdmissions(int monkeyId)
        {
               return await Admission.CreateAdmission(new AdmissionRequest { AdmittanceDate = DateTime.Today, MonkeyId = monkeyId })
                .OnSuccessTry(async result => await _admissionsRepository.AddAdmittance(result));
        }
    }
}
