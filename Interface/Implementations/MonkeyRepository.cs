using CSharpFunctionalExtensions;
using Domain.Entities;
using Domain.Models;
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

            await CarefulSaveChanges();
            return monkey.Id;

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

        public async Task<List<MonkeyReportResponse>> GetMonkeysByDate(DateTime dateFrom, DateTime dateTo)
        {
            return await _monkeyShelterDbContext.Admissions.Include(d => d.Monkey)
                .Where(p => p.MonkeyAdmittanceDate >= dateFrom && p.MonkeyAdmittanceDate <= dateTo)
                .Select(o => new MonkeyReportResponse
                {
                    MonkeyName = o.Monkey.Name,
                    Species = o.Monkey.Species,
                    MonkeyId = o.MonkeyId
                })
                .ToListAsync();
        }

        public async Task<List<MonkeyReportResponse>> GetMonkeysBySpecies(MonkeySpecies species)
        {
            return await _monkeyShelterDbContext.Monkeys.Where(p => p.Species == species)
                .Select(p => new MonkeyReportResponse { MonkeyName = p.Name, Species = p.Species })
                .ToListAsync();
        }

        public async Task RemoveMonkeyFromShelter(Monkey monkey)
        {
            Departure departure = new Departure(monkey.Id);

            _monkeyShelterDbContext.Departures.Add(departure);

            _monkeyShelterDbContext.Monkeys.Remove(monkey);

            await CarefulSaveChanges();

        }

        public async Task UpdateMonkey(MonkeyWeightRequest monkey)
        {
            var monkeyFromDb = await _monkeyShelterDbContext.Monkeys.FirstOrDefaultAsync(p => p.Id == monkey.MonkeyId);

            monkeyFromDb.Weight = monkey.NewMonkeyWeight;

            await CarefulSaveChanges();
        }

        private async Task CarefulSaveChanges()
        {
            try
            {
                await _monkeyShelterDbContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError($"An exception occurred while saving the changes , {ex.Message} ");
                throw;
            }

        }
    }
}
