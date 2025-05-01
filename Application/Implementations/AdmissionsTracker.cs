using Application.Contracts.Business;
using Application.Contracts.Repositories;
using CSharpFunctionalExtensions;
using Domain.DatabaseModels;
using Domain.Entities;
using System.Threading.Tasks;

namespace Application.Implementations
{
    public class AdmissionsTracker : IAdmissionTracker
    {
        private readonly IAdmissionsRepository _admissionsRepository;

        private const int maxNumberOfAdmittance = 7;


        public AdmissionsTracker(IAdmissionsRepository admissionsRepository)
        {
            _admissionsRepository = admissionsRepository;
        }

        private bool CanMonkeyBeAdmitted()
        {
            return _admissionsRepository.GetTodayAdmittanceAmount() <= maxNumberOfAdmittance;
        }

        public async Task<Result> Admit(int monkeyId)
        {
            if (!CanMonkeyBeAdmitted())
            {
                return Result.Failure("Shelter is currently full");
            }

            var admittanceResult = Admission.Create(monkeyId, DateTime.Today);

            if (admittanceResult.IsFailure)
            {
                return Result.Failure(admittanceResult.Error);
            }

            await _admissionsRepository.AddAdmittance(new AdmissionDbModel(monkeyId, DateTime.Today));
            return Result.Success();
        }
    }
}
