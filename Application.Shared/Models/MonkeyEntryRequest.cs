
using Domain.Enums;

namespace Application.Shared.Models
{
    public class MonkeyEntryRequest
    {
        public required string Name { get; set; }

        public required MonkeySpecies Species { get; set; }

        public required double Weight { get; set; }
    }
}
