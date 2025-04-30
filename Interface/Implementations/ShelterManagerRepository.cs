using Application.Contracts.Repositories;
using Domain.DatabaseModels;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Implementations
{
    public class ShelterManagerRepository : IShelterManagerRepository
    {
        private readonly MonkeyShelterDbContext _dbContext;

        public ShelterManagerRepository(MonkeyShelterDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ShelterManagerDbModel> CreateShelterManager(ShelterManagerDbModel shelterManager)
        {
            _dbContext.ShelterManagers.Add(shelterManager);

            await _dbContext.SaveChangesAsync();

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
        await _dbContext.SaveChangesAsync();
    }

    }
}
