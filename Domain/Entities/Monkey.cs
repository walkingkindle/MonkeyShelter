using CSharpFunctionalExtensions;
using Domain.Enums;

namespace Domain.Entities
{
    public class Monkey
    {
        public int Id { get; set; }

        public MonkeySpecies Species { get; set; }

        public string Name { get; set; }

        public double Weight { get; set; }

        public DateTime? LastUpdateTime { get; set; }

        public int ShelterId { get; set; }

        public static Result<Monkey> CreateMonkey(Maybe<string> monkeyName, Maybe<double> monkeyWeight, Maybe<MonkeySpecies> monkeySpecies, Maybe<int> shelterId)
        {
            return monkeyName.ToResult("Monkey cannot be null")
                .Ensure(monkey => !string.IsNullOrEmpty(monkeyName.Value), "Monkey must have a name")
                .Ensure(monkey => monkeyWeight.HasValue && monkeyWeight.Value > 0, "Monkey must have a valid weight")
                .Ensure(monkey => monkeySpecies.HasValue && Enum.IsDefined(typeof(MonkeySpecies), monkeySpecies.Value), "Invalid monkey species")
                .Ensure(monkey => monkeyWeight.Value < 1000, "We can not accept monkeys that are over a 1000kg heavy")
                .Ensure(monkey => shelterId.HasValue && shelterId.Value >= 0,"Shelter Id must be valid")
                .Map(monkey => new Monkey(species:monkeySpecies.Value, name: monkeyName.Value, weight:monkeyWeight.Value, shelterId:shelterId.Value));
        }

        public Monkey(MonkeySpecies species, string name, double weight,int shelterId)
        {
            Species = species;

            Name = name;

            Weight = weight;

            ShelterId = shelterId;

            LastUpdateTime = null;
        }



    }
}
