using Application.Contracts;
using Application.Contracts.Repositories;
using CSharpFunctionalExtensions;
using Domain.DatabaseModels;
using Domain.Entities;

namespace Application.Implementations
{
    public class ShelterService : IShelterService
    {
        private readonly IShelterManagerRepository _shelterManagerRepository;
        private readonly IShelterRepository _shelterRepository;

        public ShelterService(IShelterManagerRepository shelterManagerRepository, IShelterRepository shelterRepository)
        {
            _shelterManagerRepository = shelterManagerRepository;
            _shelterRepository = shelterRepository;
        }

        public async Task<Result<int>> CreateShelter(Maybe<string> username)
        {
            var userResult = username.ToResult("Username must not be null");
            if (userResult.IsFailure)
                return Result.Failure<int>(userResult.Error);

            var existingManager = await _shelterManagerRepository.GetShelterManagerByUsername(username.Value);
            if (existingManager != null)
                return Result.Success(existingManager.Id);

            var shelterManagerResult = ShelterManager.CreateShelterManager(username);
            if (shelterManagerResult.IsFailure)
                return Result.Failure<int>(shelterManagerResult.Error);

            var shelterManager = new ShelterManagerDbModel(
                shelterManagerResult.Value.UserName
            );

            var createdManager = await _shelterManagerRepository.CreateShelterManager(shelterManager);

            // Step 2: Create Shelter with the new manager’s ID
            var shelter = Shelter.CreateShelter(createdManager.Id);
            if (shelter.IsFailure)
                return Result.Failure<int>(shelter.Error);

            int shelterId = await _shelterRepository.AddNewShelter(new ShelterDbModel(shelterManager.Id));

            // Step 3: Update the manager to link it to the shelter (optional if needed)
            await _shelterManagerRepository.UpdateShelterId(createdManager.Id, shelterId);

            return Result.Success(createdManager.Id);
        }
        }
    }
