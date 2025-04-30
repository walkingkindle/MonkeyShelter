using Application.Contracts.Auth;
using CSharpFunctionalExtensions;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Application.Auth.Implementations
{
    public class AuthService : IAuthService
    {
        public Task<Result> AuthenticateAsync(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
