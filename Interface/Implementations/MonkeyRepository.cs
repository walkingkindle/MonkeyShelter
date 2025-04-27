using Domain.Entities;
using Infrastructure.Contracts;

namespace Infrastructure.Implementations
{
    public class MonkeyRepository : IMonkeyRepository
    {
        private readonly MonkeyShelterDbContext _monkeyShelterDbContext;

        public MonkeyRepository(MonkeyShelterDbContext monkeyShelterDbContext)
        {
            _monkeyShelterDbContext = monkeyShelterDbContext;
        }

        public async Task<int> AddMonkeyToShelter(Monkey monkey)
        {
            _monkeyShelterDbContext.Monkeys.Add(monkey);

            await _monkeyShelterDbContext.SaveChangesAsync();

            return monkey.Id;

        }

        public Task RemoveMonkeyFromShelter(Monkey monkey)
        {
            throw new NotImplementedException();
        }

        public Task UpdateMonkey(Monkey monkey)
        {
            throw new NotImplementedException();
        }
    }
}
