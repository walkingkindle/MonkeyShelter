using Application.Contracts;
using Application.Contracts.Repositories;
using Application.Shared.Models;

namespace Application.Implementations
{
    public class CheckupService : ICheckupService
    {
        private readonly IAdmissionsRepository _admissionsRepository;

        public CheckupService(IAdmissionsRepository admissionsRepository)
        {
            _admissionsRepository = admissionsRepository;
        }

        public async Task<MonkeyVeterinaryCheckup> RetreiveMonkeysWithUpcomingVeteranaryCheckup()
        {
            var admissionsNext30 = await _admissionsRepository.GetMonkeysByCheckupDate(DateTime.Today, DateTime.Today.AddDays(30));

            var upcomingAdmissions = await _admissionsRepository.GetMonkeysByCheckupDate(DateTime.Today, DateTime.Today.AddMonths(9));

            return new MonkeyVeterinaryCheckup { ScheduledInLessThan30Days = admissionsNext30, ScheduledInMoreThan30Days = upcomingAdmissions };
        }
    }
}
