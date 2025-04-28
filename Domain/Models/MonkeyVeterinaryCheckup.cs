namespace Domain.Models
{
    public class MonkeyVeterinaryCheckup
    {
        public List<MonkeyCheckupResponse> ScheduledInLessThan30Days { get; set; }

        public List<MonkeyCheckupResponse> ScheduledInMoreThan30Days { get; set; }

    }
}
