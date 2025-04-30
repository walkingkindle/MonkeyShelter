using Application.Contracts.Repositories;
using Domain.DatabaseModels;
using Domain.Entities;
using System.Threading.Tasks;

namespace Infrastructure.Implementations
{
    public class DeparturesRepository : IDeparturesRepository
    {
        private readonly MonkeyShelterDbContext _dbContext;
        private readonly IDbHelper _dbHelper;
        public DeparturesRepository(MonkeyShelterDbContext dbContext, IDbHelper dbHelper)
        {
            _dbContext = dbContext;
            _dbHelper = dbHelper;
            
        }
        public int GetTodayDeparturesAmount()
        {
            return _dbContext.Departures.Where(p => p.DepartureDate == DateTime.Today).Count();
        }

        public async Task Depart(int monkeyId)
        {
             DepartureDbModel departure = new DepartureDbModel(monkeyId, DateTime.Today);

            _dbContext.Departures.Add(departure);

            await _dbHelper.CarefulSaveChanges(_dbContext);
        }


    }
}
