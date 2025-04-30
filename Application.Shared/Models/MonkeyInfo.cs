using Domain.Enums;

namespace Application.Shared.Models
{
    public abstract class MonkeyInfo
    {
        public abstract int Id { get; set; }

        public abstract string Name { get; set; }

        public virtual DateTime CheckupTime { get; set; }

        public virtual MonkeySpecies Species { get; set; }
    }
}
