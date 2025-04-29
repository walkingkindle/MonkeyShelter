using CSharpFunctionalExtensions;
using Domain.Entities;
using Domain.Models;

namespace Infrastructure.Contracts
{
    public interface IMonkeyRepository
    {
        public Task<Result<int>> AddMonkeyToShelter(Monkey monkey);

        public Task RemoveMonkeyFromShelter(Monkey monkey);

        public Task<Result> UpdateMonkey(MonkeyWeightRequest monkey);

        public Task<Result<Monkey>> GetMonkeyById(Result<int> id);
        Task<List<MonkeyReportResponse>> GetMonkeysBySpecies(MonkeySpecies species);
        Task<List<MonkeyReportResponse>> GetMonkeysByDate(DateTime dateFrom, DateTime dateTo);
    }
}
