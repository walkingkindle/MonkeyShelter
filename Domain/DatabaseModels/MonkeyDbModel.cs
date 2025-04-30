using Domain.Enums;

namespace Domain.DatabaseModels
{
    public class MonkeyDbModel
    {
        public MonkeyDbModel(MonkeySpecies species, string name, double weight, DateTime? lastUpdateTime, int shelterId)
        {
            Species = species;
            Name = name;
            Weight = weight;
            LastUpdateTime = lastUpdateTime;
            ShelterId = shelterId;
        }

        public int Id { get; set; }

        public MonkeySpecies Species { get; set; }

        public string Name { get; set; }

        public double Weight { get; set; }

        public DateTime? LastUpdateTime { get; set; }

        public int ShelterId { get; set; }
    }
}