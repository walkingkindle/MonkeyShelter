using CSharpFunctionalExtensions;
using Domain.Models;

namespace Application.Contracts{
    public interface IMonkeyService
    {
        public Task<Result> AddMonkey(Maybe<MonkeyEntryRequest> request);

        public Task<Result> DepartMonkey(Maybe<MonkeyDepartureRequest> request);

        public Result AdjustMonkeyWeight();

    }
}
