using Domain.Entities;
using Domain.Models;

namespace Infrastructure
{
    public class InitialDbDataSeed
{
    private readonly MonkeyShelterDbContext _dbContext;
    private readonly Random _random;
    
    private static readonly List<string> RandomNames = new List<string>
    {
        "George", "Bella", "Milo", "Luna", "Leo", "Zara", "Rocky", "Ruby", "Max", "Charlie", 
        "Lily", "Sammy", "Bobo", "Coco", "Toby", "Daisy", "Chester", "Buddy", "Maggie", "Rex"
    };

    private static readonly List<MonkeySpecies> SpeciesList = Enum.GetValues(typeof(MonkeySpecies)).Cast<MonkeySpecies>().ToList();

    public InitialDbDataSeed(MonkeyShelterDbContext dbContext)
    {
        _dbContext = dbContext;
        _random = new Random();
    }

    public void Seed()
    {
        if (_dbContext.Monkeys.Any())
        {
            return;
        }
        var monkeys = new List<Monkey>();

        for (int i = 0; i < 90; i++)
        {
            var monkey = Monkey.CreateMonkey(new MonkeyEntryRequest { Name = GetRandomName(), Species = GetRandomSpecies(), Weight = GetRandomWeight() }).Value;
            monkeys.Add(monkey);
        }

        _dbContext.Monkeys.AddRange(monkeys);
        _dbContext.SaveChanges();
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
        // Random weight between 5 kg and 50 kg
        return _random.NextDouble() * (50 - 5) + 5;
    }
}

}
