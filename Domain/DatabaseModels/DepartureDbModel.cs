namespace Domain.DatabaseModels
{
    public class DepartureDbModel
    {
        public DepartureDbModel(int monkeyId, DateTime departureDate)
        {
            MonkeyId = monkeyId;
            DepartureDate = departureDate;
        }

        public int Id { get; set; }
        public int MonkeyId { get; set; }

        public MonkeyDbModel Monkey { get; set; }
        public DateTime DepartureDate { get; set; }

    }
}
