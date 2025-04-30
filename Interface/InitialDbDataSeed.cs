using Application.Contracts.Repositories;
using Application.Shared.Models;
using CSharpFunctionalExtensions;
using Domain.DatabaseModels;
using Domain.Entities;
using Domain.Enums;
using System.Threading.Tasks;

namespace Infrastructure
{
    public sealed class InitialDbDataSeed
{
    private readonly MonkeyShelterDbContext _dbContext;
    private readonly Random _random;
    private readonly IMonkeyRepository _monkeyRepository;
    private readonly IAdmissionsRepository _admissionsRepository;
    
    private static readonly List<string> RandomNames = new List<string>
    {
        "George", "Bella", "Milo", "Luna", "Leo", "Zara", "Rocky", "Ruby", "Max", "Charlie", 
        "Lily", "Sammy", "Bobo", "Coco", "Toby", "Daisy", "Chester", "Buddy", "Maggie", "Rex"
    };

    private static readonly List<MonkeySpecies> SpeciesList = Enum.GetValues(typeof(MonkeySpecies)).Cast<MonkeySpecies>().ToList();

    public InitialDbDataSeed(MonkeyShelterDbContext dbContext, IMonkeyRepository monkeyRepository, IAdmissionsRepository admissionsRepository)
    {
        _dbContext = dbContext;
        _random = new Random();
        _monkeyRepository = monkeyRepository;
        _admissionsRepository = admissionsRepository;
    }

      public async Task Seed()
      {
        if (_dbContext.Monkeys.Any()) return;

        // Step 1: Seed shelter managers
        var shelterManagers = Enumerable.Range(1, 9)
            .Select(i => new ShelterManagerDbModel(GetRandomName()))
            .ToList();
        await _dbContext.ShelterManagers.AddRangeAsync(shelterManagers);
        await _dbContext.SaveChangesAsync();

        // Step 2: Seed shelters and assign managers
        var shelters = shelterManagers
            .Select(manager => new ShelterDbModel(manager.Id))
            .ToList();
        await _dbContext.Shelters.AddRangeAsync(shelters);
        await _dbContext.SaveChangesAsync();

        // Step 3: Seed monkeys with admissions
        var monkeys = new List<MonkeyDbModel>();
        var admissions = new List<AdmissionDbModel>();
        var rng = new Random();

        for (int i = 0; i < 90; i++)
        {
            var shelter = shelters[rng.Next(shelters.Count)];
            var monkey = new MonkeyDbModel(GetRandomSpecies(), GetRandomName(), GetRandomWeight(), null, shelter.Id);
            monkeys.Add(monkey);
        }

        await _dbContext.AddRangeAsync(monkeys);
        await _dbContext.SaveChangesAsync();

        foreach (var monkey in monkeys)
        {
            var randomAdmissionDate = DateTime.UtcNow.AddDays(-rng.Next(0, 90));
            var admission = new AdmissionDbModel(monkey.Id, randomAdmissionDate);
            admissions.Add(admission);
        }

        await _dbContext.AddRangeAsync(admissions);
        await _dbContext.SaveChangesAsync();
      }

      private int GetRandomShelterId()
        {
            return _random.Next(1, 11);
        }

        private MonkeySpecies GetRandomSpecies()
    {
        return SpeciesList[_random.Next(SpeciesList.Count)];
    }

    private string GetRandomName()
    {
        return RandomNames[_random.Next(RandomNames.Count)];
    }

    private double GetRandomWeight()
    {
        return _random.NextDouble() * (50 - 5) + 5;
    }
}

}
