using Domain.Entities;
using Domain.Models;
using Infrastructure.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Implementations
{
    public class AdmissionsRepository : IAdmissionsRepository
    {
        private readonly MonkeyShelterDbContext _monkeyShelterDbContext;

        public AdmissionsRepository(MonkeyShelterDbContext monkeyShelterDbContext)
        {
            _monkeyShelterDbContext = monkeyShelterDbContext;
        }

        public async Task AddAdmittance(Admission admission)
        {
            _monkeyShelterDbContext.Admissions.Add(admission);

            await _monkeyShelterDbContext.SaveChangesAsync();
        }

        public int GetMonkeysAmountBySpecies(MonkeySpecies species)
        {
            return _monkeyShelterDbContext.Monkeys.Where(p => p.Species == species).Count();
        }

        public async Task<List<MonkeyCheckupResponse>> GetMonkeysByCheckupDate(DateTime dateFrom, DateTime dateTo)
        {
            return await _monkeyShelterDbContext.Admissions
                .Where(p => p.MonkeyCheckupTime >= dateFrom && p.MonkeyCheckupTime <= dateTo)
                .Select(z => new MonkeyCheckupResponse { MonkeyId = z.MonkeyId, MonkeyName = z.Monkey.Name, CheckupTime = z.MonkeyCheckupTime})
                .ToListAsync();
        }

        public int GetTodayAdmittanceAmount()
        {
            return _monkeyShelterDbContext.Admissions.Where(p => p.MonkeyAdmittanceDate == DateTime.Today).Count();
        }

     }
}
