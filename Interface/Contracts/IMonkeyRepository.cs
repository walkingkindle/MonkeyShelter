using CSharpFunctionalExtensions;
using Domain.Entities;
using Domain.Models;

namespace Infrastructure.Contracts
{
    public interface IMonkeyRepository
    {
        public Task<int> AddMonkeyToShelter(Monkey monkey);

        public Task RemoveMonkeyFromShelter(Monkey monkey);

        public Task UpdateMonkey(MonkeyWeightRequest request);

        public Task<Result<Monkey>> GetMonkeyById(int id);
        Task<List<MonkeyReportResponse>> GetMonkeysBySpecies(MonkeySpecies species);
        Task<List<MonkeyReportResponse>> GetMonkeysByDate(DateTime dateFrom, DateTime dateTo);
    }
}
