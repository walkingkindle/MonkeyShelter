using Application.Shared.Models;
using Domain.DatabaseModels;
using Domain.Entities;
using Domain.Enums;

namespace Application.Contracts.Repositories
{
    public interface IAdmissionsRepository
    {
        public int GetTodayAdmittanceAmount();

        public Task AddAdmittance(AdmissionDbModel admission);

        int GetMonkeysAmountBySpecies(MonkeySpecies species);

        Task<List<MonkeyCheckupResponse>> GetMonkeysByCheckupDate(DateTime dateFrom, DateTime dateTo);

        Task AddRangeAdmissions(List<AdmissionDbModel> admissions);

    }
}
