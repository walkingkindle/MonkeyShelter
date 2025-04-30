using CSharpFunctionalExtensions;

namespace Application.Contracts.Business
{
    public interface IShelterService{
        Task<Result<int>> CreateShelter(Maybe<string> username);
    }
}
