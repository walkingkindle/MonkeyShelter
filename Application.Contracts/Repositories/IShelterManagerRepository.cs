using Domain.DatabaseModels;
using Domain.Entities;

namespace Application.Contracts.Repositories
{
    public interface IShelterManagerRepository
    {
        Task<ShelterManagerDbModel> CreateShelterManager(ShelterManagerDbModel shelterManager);
        Task<ShelterManagerDbModel?> GetShelterManagerByUsername(string username);

        Task UpdateShelterId(int managerId, int shelterId);
    }
}
