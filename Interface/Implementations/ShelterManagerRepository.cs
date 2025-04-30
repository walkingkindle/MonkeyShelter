using Application.Contracts.Repositories;
using Domain.DatabaseModels;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Implementations
{
    public class ShelterManagerRepository : IShelterManagerRepository
    {
        private readonly MonkeyShelterDbContext _dbContext;
        private readonly IDbHelper _dbHelper;

        public ShelterManagerRepository(MonkeyShelterDbContext dbContext, IDbHelper dbHelper)
        {
            _dbContext = dbContext;
            _dbHelper = dbHelper;
        }

        public async Task<ShelterManagerDbModel> CreateShelterManager(ShelterManagerDbModel shelterManager)
        {
            _dbContext.ShelterManagers.Add(shelterManager);

            await _dbHelper.CarefulSaveChanges(_dbContext);
            return shelterManager;
        }

        public async Task<ShelterManagerDbModel?> GetShelterManagerByUsername(string name)
        {
            return await _dbContext.ShelterManagers.FirstOrDefaultAsync(p => p.Name == name);
        }

        public async Task UpdateShelterId(int managerId, int shelterId)
        {
            var manager = await _dbContext.ShelterManagers.FindAsync(managerId);
            if (manager == null)
            {
                throw new InvalidOperationException("Shelter manager not found");
            }
            manager.ShelterId = shelterId;
            await _dbHelper.CarefulSaveChanges(_dbContext);
        }

    }
}
