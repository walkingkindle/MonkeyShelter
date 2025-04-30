using Domain.DatabaseModels;
using Domain.Entities;

namespace Application.Contracts.Repositories
{
    public interface IShelterRepository
    {
        public Task<int> AddNewShelter(ShelterDbModel shelter);
    }
}
