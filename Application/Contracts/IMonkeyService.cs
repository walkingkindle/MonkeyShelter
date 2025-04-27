using CSharpFunctionalExtensions;
using Domain.Models;

namespace Application.Contracts{
    public interface IMonkeyService
    {
        public Task<Result> AddMonkey(Maybe<MonkeyEntryRequest> request);

        public Result DepartMonkey();

        public Result AdjustMonkeyWeight();

    }
}
