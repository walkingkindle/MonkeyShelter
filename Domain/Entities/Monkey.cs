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

        public DateTime? LastUpdateTime { get; set; }

        public static Result<Monkey> CreateMonkey(Maybe<MonkeyEntryRequest> request)
        {
            return request.ToResult("Monkey cannot be null")
                .Ensure(monkey => !string.IsNullOrEmpty(monkey.Name), "Monkey must have a name")
                .Ensure(monkey => monkey.Weight > 0, "Monkey must have a valid weight")
                .Ensure(monkey => Enum.IsDefined(typeof(MonkeySpecies), monkey.Species), "Invalid monkey species")
                .Ensure(monkey => monkey.Weight < 1000,"We can not accept monkeys that are over a 1000kg heavy")
                .Map(monkey => new Monkey(species:monkey.Species, name: monkey.Name, weight: monkey.Weight));
        }

        public Monkey(MonkeySpecies species, string name, double weight)
        {
            Species = species;

            Name = name;

            Weight = weight;

            LastUpdateTime = null;
        }



    }
}
