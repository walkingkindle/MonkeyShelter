using Application.Contracts.Auth;
using Application.Contracts.Repositories;

namespace Application.Auth.Implementations
{
    public class ShelterAuthorizationService : IShelterAuthorizationService
    {
        private readonly IShelterRepository _shelterRepository;

        public ShelterAuthorizationService(IShelterRepository shelterRepository)
        {
            _shelterRepository = shelterRepository;
        }

        public async Task<bool> IsMonkeyOwnedByShelterAsync(int monkeyId, int shelterId)
        {
            return await _shelterRepository.IsMonkeyOwnedByShelterAsync(monkeyId, shelterId);
        }
    }
}
