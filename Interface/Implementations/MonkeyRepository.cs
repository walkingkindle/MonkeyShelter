using Application.Contracts.Repositories;
using Application.Shared.Models;
using CSharpFunctionalExtensions;
using Domain.DatabaseModels;
using Domain.Entities;
using Domain.Enums;
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

        public async Task<Result<int>> AddMonkeyToShelter(MonkeyDbModel monkey)
        {
           _monkeyShelterDbContext.Monkeys.Add(monkey);

            await CarefulSaveChanges();
            return Result.Success(monkey.Id);

        }

        public async Task AddRangeMonkeys(List<MonkeyDbModel> monkeys)
        {
            _monkeyShelterDbContext.AddRange(monkeys);

            await CarefulSaveChanges();
        }

        public async Task<Result<MonkeyDbModel>> GetMonkeyById(int monkeyId)
        {
            var monkey = await _monkeyShelterDbContext.Monkeys.FirstOrDefaultAsync(p => p.Id == monkeyId);

            if(monkey == null)
            {
                return Result.Failure<MonkeyDbModel>("Monkey with the specified Id could not be found");
            }

            return Result.Success(monkey);

        }

        public async Task<List<MonkeyReportResponse>> GetMonkeysByDate(DateTime dateFrom, DateTime dateTo)
        {
            return await _monkeyShelterDbContext.Admissions.Include(o => o.Monkey)
                .Where(p => p.MonkeyAdmittanceDate >= dateFrom && p.MonkeyAdmittanceDate <= dateTo)
                .Select(o => new MonkeyReportResponse
                {
                    Name = o.Monkey.Name,
                    Weight = o.Monkey.Weight,
                    Species = o.Monkey.Species,
                    Id = o.Monkey.Id,
                    LastEditDate = o.Monkey.LastUpdateTime
                })
                .ToListAsync();
        }

        public async Task<List<MonkeyReportResponse>> GetMonkeysBySpecies(MonkeySpecies species)
        {
            return await _monkeyShelterDbContext.Monkeys.Where(p => p.Species == species)
                .Select(p => new MonkeyReportResponse { Id = p.Id, Name = p.Name, Species = p.Species, LastEditDate = p.LastUpdateTime })
                .ToListAsync();
        }

        public async Task RemoveMonkeyFromShelter(MonkeyDbModel monkey)
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
            monkeyFromDb.LastUpdateTime = DateTime.Now;

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
