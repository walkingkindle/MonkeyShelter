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
            return _admissionsRepository.GetTodayAdmittanceAmount() <= maxNumberOfAdmittance;
        }
        public async Task<Result> Admit(Maybe<int> monkeyId)
        {
               return await monkeyId.ToResult("Monkey Id cannot be null")
                .Ensure(p => p >= 0,"monkey id must be valid")
                .Map(monkeyId => Admission.CreateAdmission(new AdmissionRequest { AdmittanceDate = DateTime.Today, MonkeyId = monkeyId }))
                .OnSuccessTry(async monkeyId => await _admissionsRepository.AddAdmittance(monkeyId.Value));
        }
    }
}
