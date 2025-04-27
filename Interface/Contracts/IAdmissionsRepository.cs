using Domain.Entities;

namespace Infrastructure.Contracts
{
    public interface IAdmissionsRepository
    {
        public int GetTodayAdmittanceAmount();

        public Task AddAdmittance(Admission admission);
    }
}
