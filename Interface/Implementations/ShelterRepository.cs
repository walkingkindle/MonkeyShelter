using Application.Contracts.Repositories;
using Domain.DatabaseModels;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Implementations
{
    public class ShelterRepository : IShelterRepository
    {
        private readonly MonkeyShelterDbContext _dbContext;
        private readonly IDbHelper _dbHelper;

        public ShelterRepository(MonkeyShelterDbContext dbContext, IDbHelper dbHelper)
        {
            _dbContext = dbContext;
            _dbHelper = dbHelper;
        }

        public async Task<int> AddNewShelter(ShelterDbModel shelter)
        {
            _dbContext.Add(shelter);

            await _dbHelper.CarefulSaveChanges(_dbContext);

            return shelter.Id;

        }

        public async Task<bool> IsMonkeyOwnedByShelterAsync(int monkeyId, int shelterId)
        {
            return await _dbContext.Monkeys.AnyAsync(x => x.Id == monkeyId && x.ShelterId == shelterId);
        }
    }
}
