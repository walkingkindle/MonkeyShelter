using Domain.Entities;

namespace Domain.Models
{
    public abstract class MonkeyInfo
    {
        public abstract int MonkeyId { get; set; }

        public abstract string MonkeyName { get; set; }

        public virtual DateTime CheckupTime { get; set; }

        public virtual MonkeySpecies Species { get; set; }
    }
}
