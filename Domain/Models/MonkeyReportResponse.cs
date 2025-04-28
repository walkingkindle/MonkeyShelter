using Domain.Entities;

namespace Domain.Models
{
    public class MonkeyReportResponse : MonkeyInfo
    {
        public override int MonkeyId { get; set; }
        public override string MonkeyName { get; set; }

        public override MonkeySpecies Species { get; set; }
    }
}
