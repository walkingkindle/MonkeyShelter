using CSharpFunctionalExtensions;

namespace Application.Contracts.Auth
{
    public interface IAuthService
    {
        Task<Result> AuthenticateAsync(string username, string password);
    }
}
