using CSharpFunctionalExtensions;
using Domain.Entities;
using Infrastructure.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Implementations
{
    public class MonkeyRepository : IMonkeyRepository
    {
        private readonly MonkeyShelterDbContext _monkeyShelterDbContext;
        private readonly ILogger<MonkeyRepository> _logger;

        public MonkeyRepository(MonkeyShelterDbContext monkeyShelterDbContext, ILogger<MonkeyRepository> logger)
        {
            _monkeyShelterDbContext = monkeyShelterDbContext;
            _logger = logger;

        }

        public async Task<int> AddMonkeyToShelter(Monkey monkey)
        {
            _monkeyShelterDbContext.Monkeys.Add(monkey);

            try
            {
                await _monkeyShelterDbContext.SaveChangesAsync();

                return monkey.Id;
            }
            catch(Exception ex)
            {

                _logger.LogError($"Database save failed,{ex.Message}");

                throw;
            }

        }

        public async Task<Result<Monkey>> GetMonkeyById(int id)
        {
            var monkey = await _monkeyShelterDbContext.Monkeys.FirstOrDefaultAsync(p => p.Id == id);

            if(monkey == null)
            {
                return Result.Failure<Monkey>("Monkey does not exist");
            }

            return Result.Success(monkey);

        }

        public async Task RemoveMonkeyFromShelter(Monkey monkey)
        {
            Departure departure = new Departure(monkey.Id);

            _monkeyShelterDbContext.Departures.Add(departure);

            _monkeyShelterDbContext.Monkeys.Remove(monkey);

            try
            {
                await _monkeyShelterDbContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError($"An exception occurred while saving the changes to departure of MonkeyId :{monkey.Id}, {ex.Message} ");
                throw;
            }

        }

        public Task UpdateMonkey(Monkey monkey)
        {
            throw new NotImplementedException();
        }
    }
}
