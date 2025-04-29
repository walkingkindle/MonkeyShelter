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

        public async Task<Result<int>> AddMonkeyToShelter(Monkey monkey)
        {
             _monkeyShelterDbContext.Monkeys.Add(monkey);

            await CarefulSaveChanges();
            return Result.Success(monkey.Id);

        }

        public async Task<Result<Monkey>> GetMonkeyById(Result<int> id)
        {
            if (id.IsFailure)
            {
                return Result.Failure<Monkey>(id.Error);
            }
            var monkey = await _monkeyShelterDbContext.Monkeys.FirstOrDefaultAsync(p => p.Id == id.Value);

            if(monkey == null)
            {
                return Result.Failure<Monkey>("Monkey with the specified Id could not be found");
            }

            return Result.Success(monkey);

        }

        public async Task<List<MonkeyReportResponse>> GetMonkeysByDate(DateTime dateFrom, DateTime dateTo)
        {
            return await _monkeyShelterDbContext.Admissions.Include(d => d.Monkey)
                .Where(p => p.MonkeyAdmittanceDate >= dateFrom && p.MonkeyAdmittanceDate <= dateTo)
                .Select(o => new MonkeyReportResponse
                {
                    Name = o.Monkey.Name,
                    Species = o.Monkey.Species,
                    Id = o.MonkeyId
                })
                .ToListAsync();
        }

        public async Task<List<MonkeyReportResponse>> GetMonkeysBySpecies(MonkeySpecies species)
        {
            return await _monkeyShelterDbContext.Monkeys.Where(p => p.Species == species)
                .Select(p => new MonkeyReportResponse { Name = p.Name, Species = p.Species })
                .ToListAsync();
        }

        public async Task RemoveMonkeyFromShelter(Monkey monkey)
        {
            _monkeyShelterDbContext.Monkeys.Remove(monkey);

            await CarefulSaveChanges();

        }

        public async Task<Result> UpdateMonkey(MonkeyWeightRequest monkey)
        {
            var monkeyFromDb = await _monkeyShelterDbContext.Monkeys.FirstOrDefaultAsync(p => p.Id == monkey.MonkeyId);

            if(monkeyFromDb == null)
            {
                return Result.Failure("Could not find the monkey with the specified Id");
            }
            monkeyFromDb.Weight = monkey.NewMonkeyWeight;

            await CarefulSaveChanges();

            return Result.Success();
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
