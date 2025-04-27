using CSharpFunctionalExtensions;
using Domain.Models;

namespace Domain.Entities
{
    public class Monkey
    {
        public int Id { get; set; }

        public MonkeySpecies Species { get; set; }

        public string Name { get; set; }

        public double Weight { get; set; }

        public DateTime CheckupTime { get; set; }

        public static Result<Monkey> CreateMonkey(Maybe<MonkeyEntryRequest> request)
        {
            return request.ToResult("Monkey cannot be null")
                .Ensure(monkey => !string.IsNullOrEmpty(monkey.Name), "Monkey must have a name")
                .Ensure(monkey => monkey.Weight > 0, "Monkey must have a valid weight")
                .Ensure(monkey => Enum.IsDefined(typeof(MonkeySpecies), monkey.Species), "Invalid monkey species")
                .Map(monkey => new Monkey(species:monkey.Species, name: monkey.Name, weight: monkey.Weight));
        }

        public Monkey(MonkeySpecies species, string name, double weight)
        {
            Species = species;

            Name = name;

            Weight = weight;

            CheckupTime = CalculateCheckupTime();
        }

        private DateTime CalculateCheckupTime()
        {
            return DateTime.Today.AddMonths(6);
        }

    }
}
