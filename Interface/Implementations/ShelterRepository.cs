using Application.Contracts.Repositories;
using Domain.DatabaseModels;

namespace Infrastructure.Implementations
{
    public class ShelterRepository : IShelterRepository
    {
        private readonly MonkeyShelterDbContext _dbContext;

        public ShelterRepository(MonkeyShelterDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddNewShelter(ShelterDbModel shelter)
        {
            _dbContext.Add(shelter);

            await _dbContext.SaveChangesAsync();

            return shelter.Id;
            
        }
    }
}
