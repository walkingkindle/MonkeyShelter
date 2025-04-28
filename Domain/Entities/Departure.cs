namespace Domain.Entities
{
    public class Departure
    {
        public int Id { get; set; }
        public Monkey Monkey { get; set; }

        public int MonkeyId { get; set; }

        public DateTime DepartureDate { get; set; }

        public Departure(int monkeyId)
        {
            MonkeyId = monkeyId;

            DepartureDate = DateTime.Today;
            
        }
    }
}
