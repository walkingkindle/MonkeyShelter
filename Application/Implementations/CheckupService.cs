using Application.Contracts;
using Application.Contracts.Business;
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
            var upcomingAdmissions = await _admissionsRepository.GetMonkeysByCheckupDate(DateTime.Today, DateTime.Today.AddMonths(3));

            return new MonkeyVeterinaryCheckup { ScheduledCheckups = upcomingAdmissions };
        }
    }
}
