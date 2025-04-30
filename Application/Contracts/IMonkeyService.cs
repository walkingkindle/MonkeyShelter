using Application.Implementations;
using Application.Shared.Models;
using CSharpFunctionalExtensions;
using Domain.Enums;

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
