using CSharpFunctionalExtensions;
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

    for (int i = 1; i <= 90; i++)
    {
        var monkeyResult = Monkey.CreateMonkey(new MonkeyEntryRequest 
        { 
            Name = GetRandomName(), 
            Species = GetRandomSpecies(), 
            Weight = GetRandomWeight() 
        });

        if (monkeyResult.IsFailure)
        {
            throw new ArgumentException($"Error creating monkey: {monkeyResult.Error}");
        }

        monkeys.Add(monkeyResult.Value);
    }

    _dbContext.Monkeys.AddRange(monkeys);
    _dbContext.SaveChanges();
    var admissions = new List<Admission>();
    foreach (var monkey in monkeys)
    {
        var admissionResult = Admission.CreateAdmission(new AdmissionRequest 
        { 
            MonkeyId = monkey.Id, 
            AdmittanceDate = DateTime.Today.AddDays(-monkey.Id)
        });

        if (admissionResult.IsFailure)
        {
            throw new ArgumentException($"Error creating admission: {admissionResult.Error}");
        }

        admissions.Add(admissionResult.Value);
    }

    _dbContext.Admissions.AddRange(admissions);
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
        return _random.NextDouble() * (50 - 5) + 5;
    }
}

}
