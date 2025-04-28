using CSharpFunctionalExtensions;
using Domain.Entities;

namespace Infrastructure.Contracts
{
    public interface IMonkeyRepository
    {
        public Task<int> AddMonkeyToShelter(Monkey monkey);

        public Task RemoveMonkeyFromShelter(Monkey monkey);

        public Task UpdateMonkey(Monkey monkey);

        public Task<Result<Monkey>> GetMonkeyById(int id);


    }
}
