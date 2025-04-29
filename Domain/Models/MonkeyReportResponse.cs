using Domain.Entities;

namespace Domain.Models
{
    public class MonkeyReportResponse : MonkeyInfo
    {
        public override int Id { get; set; }
        public override string Name { get; set; }

        public override MonkeySpecies Species { get; set; }
    }
}
