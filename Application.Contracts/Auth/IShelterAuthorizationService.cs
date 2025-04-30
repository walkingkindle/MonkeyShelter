namespace Application.Contracts.Auth
{
    public interface IShelterAuthorizationService
    {
        Task<bool> IsMonkeyOwnedByShelterAsync(int monkeyId, int shelterId);
    }
}
