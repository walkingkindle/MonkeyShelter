using Application.Contracts;
using CSharpFunctionalExtensions;
using Domain.Entities;
using Infrastructure.Contracts;

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

        public Result CanMonkeyBeAdmitted()
        {
            return _admissionsRepository.GetTodayAdmittanceAmount() >= maxNumberOfAdmittance ? Result.Failure("Shelter is full") : Result.Success();

        }

        public Task<int> GetAdmissionsForToday()
        {
            throw new NotImplementedException();
        }

        public async Task IncrementAdmissions(int monkeyId)
        {
            await _admissionsRepository.AddAdmittance(new Admission { MonkeyAdmittanceDate = DateTime.Today, MonkeyId = monkeyId });
        }
    }
}
