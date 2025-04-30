using CSharpFunctionalExtensions;

namespace Application.Auth.Contracts
{
    public interface IAuthService
    {
        Task<Result> AuthenticateAsync(string username, string password);
    }
}
