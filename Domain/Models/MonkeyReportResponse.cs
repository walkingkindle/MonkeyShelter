using Domain.Entities;

namespace Domain.Models
{
    public class MonkeyReportResponse    {
        public int Id { get; set; }
        public string Name { get; set; }

        public MonkeySpecies Species { get; set; }
    }
}
