using Application.Shared.Models;
using CSharpFunctionalExtensions;
using Domain.Enums;

namespace Application.Contracts.Business { 
    public interface IMonkeyService
    {
        public Task<Result<MonkeyReportResponse>> AddMonkey(Maybe<MonkeyEntryRequest> request, int shelterId);

        public Task<Result> DepartMonkey(Maybe<MonkeyDepartureRequest> request);

        Task<Result> UpdateMonkeyWeight(Maybe<MonkeyWeightRequest> request);
        Task<Result<List<MonkeyReportResponse>>> GetMonkeyBySpecies(Maybe<MonkeySpecies> species);
        Task<Result<List<MonkeyReportResponse>>> GetMonkeysByDate(MonkeyDateRequest dateTimes);
    }
}
