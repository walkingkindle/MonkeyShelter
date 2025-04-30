
using Application.Shared.Models;

namespace Application.Contracts.Business
{
    public interface ICheckupService
    {
        public Task<MonkeyVeterinaryCheckup> RetreiveMonkeysWithUpcomingVeteranaryCheckup();
    }
}
