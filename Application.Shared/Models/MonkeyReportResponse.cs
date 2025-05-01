using Domain.Enums;


namespace Application.Shared.Models
{
    public class MonkeyReportResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public double Weight { get; set; }

        public MonkeySpecies Species { get; set; }

        public DateTime? CheckupTime { get; set; }
    }
}
