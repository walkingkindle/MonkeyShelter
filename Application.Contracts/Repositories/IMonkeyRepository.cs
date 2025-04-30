using Application.Shared.Models;
using CSharpFunctionalExtensions;
using Domain.DatabaseModels;
using Domain.Entities;
using Domain.Enums;

namespace Application.Contracts.Repositories
{
    public interface IMonkeyRepository
    {
        public Task<Result<int>> AddMonkeyToShelter(MonkeyDbModel monkey);

        public Task RemoveMonkeyFromShelter(MonkeyDbModel monkey);

        public Task<Result> UpdateMonkey(MonkeyWeightRequest monkey);

        public Task AddRangeMonkeys(List<MonkeyDbModel> monkeys);

        public Task<Result<MonkeyDbModel>> GetMonkeyById(int monkeyId);
        Task<List<MonkeyReportResponse>> GetMonkeysBySpecies(MonkeySpecies species);
        Task<List<MonkeyReportResponse>> GetMonkeysByDate(DateTime dateFrom, DateTime dateTo);
    }
}
