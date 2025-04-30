using Application.Contracts;
using Application.Contracts.Repositories;
using Application.Shared.Models;
using CSharpFunctionalExtensions;
using Domain.DatabaseModels;
using Domain.Entities;
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
                .Map(monkeyId => Admission.CreateAdmission(monkeyId, DateTime.Today))
                .OnSuccessTry(async result=> await _admissionsRepository.AddAdmittance(new AdmissionDbModel(result.Value.MonkeyId, DateTime.Today)));
        }
    }
}
