using Domain.Models;

namespace Application.Contracts
{
    public interface ICheckupService
    {
        public Task<MonkeyVeterinaryCheckup> RetreiveMonkeysWithUpcomingVeteranaryCheckup();
    }
}
