using Application.Contracts;
using Domain.Models;
using Infrastructure.Contracts;
using System.Threading.Tasks;

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
            var monkeysThatHaveVetCheckToday = await _admissionsRepository.GetMonkeysByCheckupDate(DateTime.Today, DateTime.Today.AddDays(30));

            var monkeysThatHaveVetCheckInTheNext30 = await _admissionsRepository.GetMonkeysByCheckupDate(DateTime.Today, DateTime.Today.AddMonths(9));

            return new MonkeyVeterinaryCheckup { ScheduledInLessThan30Days = monkeysThatHaveVetCheckToday, ScheduledInMoreThan30Days = monkeysThatHaveVetCheckInTheNext30 };
        }
    }
}
