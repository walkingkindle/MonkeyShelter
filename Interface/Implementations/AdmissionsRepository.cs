using Application.Contracts.Repositories;
using Application.Shared.Models;
using Domain.DatabaseModels;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Implementations
{
    public class AdmissionsRepository : IAdmissionsRepository
    {
        private readonly MonkeyShelterDbContext _monkeyShelterDbContext;
        private readonly IDbHelper _dbHelper;

        public AdmissionsRepository(MonkeyShelterDbContext monkeyShelterDbContext, IDbHelper dbHelper)
        {
            _monkeyShelterDbContext = monkeyShelterDbContext;
            _dbHelper = dbHelper;
        }

        public async Task AddAdmittance(AdmissionDbModel admission)
        {
            _monkeyShelterDbContext.Admissions.Add(admission);

            await _dbHelper.CarefulSaveChanges(_monkeyShelterDbContext);
        }

        public async Task AddRangeAdmissions(List<AdmissionDbModel> Admissions)
        {
            _monkeyShelterDbContext.Admissions.AddRange(Admissions);

            await _dbHelper.CarefulSaveChanges(_monkeyShelterDbContext);

        }

        public int GetMonkeysAmountBySpecies(MonkeySpecies species)
        {
            return _monkeyShelterDbContext.Monkeys.Where(p => p.Species == species).Count();
        }

        public async Task<List<MonkeyCheckupResponse>> GetMonkeysByCheckupDate(DateTime dateFrom, DateTime dateTo)
        {
            return await _monkeyShelterDbContext.Admissions
                .Where(p => p.MonkeyCheckupTime >= dateFrom && p.MonkeyCheckupTime <= dateTo)
                .Select(z => new MonkeyCheckupResponse { Id = z.MonkeyId, Name = z.Monkey.Name, CheckupTime = z.MonkeyCheckupTime})
                .ToListAsync();
        }

        public int GetTodayAdmittanceAmount()
        {
            return _monkeyShelterDbContext.Admissions.Where(p => p.MonkeyAdmittanceDate == DateTime.Today).Count();
        }

     }
}
