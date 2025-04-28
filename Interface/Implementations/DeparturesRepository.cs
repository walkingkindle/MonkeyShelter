using Infrastructure.Contracts;
using System.Threading.Tasks;

namespace Infrastructure.Implementations
{
    public class DeparturesRepository : IDeparturesRepository
    {
        private readonly MonkeyShelterDbContext _dbContext;
        public DeparturesRepository(MonkeyShelterDbContext dbContext)
        {
            _dbContext = dbContext;
            
        }
        public int GetTodayDeparturesAmount()
        {
            return _dbContext.Departures.Where(p => p.DepartureDate == DateTime.Today).Count();
        }
    }
}
