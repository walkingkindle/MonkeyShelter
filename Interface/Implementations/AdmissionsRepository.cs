using Domain.Entities;
using Infrastructure.Contracts;

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

        public int GetTodayAdmittanceAmount()
        {
            return _monkeyShelterDbContext.Admissions.Select(p => p.MonkeyAdmittanceDate == DateTime.Today).Count();
        }
    }
}
