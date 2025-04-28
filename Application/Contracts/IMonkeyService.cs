using CSharpFunctionalExtensions;
using Domain.Entities;
using Domain.Models;

namespace Application.Contracts{
    public interface IMonkeyService
    {
        public Task<Result> AddMonkey(Maybe<MonkeyEntryRequest> request);

        public Task<Result> DepartMonkey(Maybe<MonkeyDepartureRequest> request);

        Task<Result> UpdateMonkeyWeight(Maybe<MonkeyWeightRequest> request);
        Task<List<MonkeyReportResponse>> GetMonkeyBySpecies(Maybe<MonkeySpecies> species);
        Task<List<MonkeyReportResponse>> GetMonkeysByDate(DateTime dateFrom, DateTime dateTo);
    }
}
