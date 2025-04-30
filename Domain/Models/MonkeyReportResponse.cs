using Domain.Entities;

namespace Domain.Models
{
    public class MonkeyReportResponse    {
        public int Id { get; set; }
        public string Name { get; set; }

        public double Weight { get; set; }

        public MonkeySpecies Species { get; set; }

        public DateTime? LastEditDate { get; set; }
    }
}
