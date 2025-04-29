using Application.Implementations;
using CSharpFunctionalExtensions;
using Domain.Entities;
using Domain.Models;

namespace Application.Contracts{
    public interface IMonkeyService
    {
        public Task<Result> AddMonkey(Maybe<MonkeyEntryRequest> request);

        public Task<Result> DepartMonkey(Maybe<MonkeyDepartureRequest> request);

        Task<Result> UpdateMonkeyWeight(Maybe<MonkeyWeightRequest> request);
        Task<Result<List<MonkeyReportResponse>>> GetMonkeyBySpecies(Maybe<MonkeySpecies> species);
        Task<Result<List<MonkeyReportResponse>>> GetMonkeysByDate(MonkeyDateRequest dateTimes);
    }
}
