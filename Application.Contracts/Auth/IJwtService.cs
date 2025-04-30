namespace Application.Contracts.Auth
{
    public interface IJwtService
    {
        public string GenerateToken(string shelterManager,int shelterId);
    }
}
