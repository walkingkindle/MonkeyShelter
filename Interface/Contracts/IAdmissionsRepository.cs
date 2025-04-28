using Domain.Entities;
using Domain.Models;

namespace Infrastructure.Contracts
{
    public interface IAdmissionsRepository
    {
        public int GetTodayAdmittanceAmount();

        public Task AddAdmittance(Admission admission);

        int GetMonkeysAmountBySpecies(MonkeySpecies species);

        Task<List<MonkeyCheckupResponse>> GetMonkeysByCheckupDate(DateTime dateFrom, DateTime dateTo);

    }
}
