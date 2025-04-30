namespace Application.Auth.Contracts
{
    public interface IJwtService
    {
        public string GenerateToken(string shelterManager,int shelterId);
    }
}
