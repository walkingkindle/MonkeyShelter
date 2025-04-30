using CSharpFunctionalExtensions;
using Domain.Entities;

namespace Application.Contracts
{
    public interface IShelterService{
        Task<Result<int>> CreateShelter(Maybe<string> username);
    }
}
