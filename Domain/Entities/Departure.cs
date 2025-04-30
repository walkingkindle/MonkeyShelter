using CSharpFunctionalExtensions;

namespace Domain.Entities
{
    public class Departure
    {
        public int Id { get; set; }
        public int MonkeyId { get; set; }
        public DateTime DepartureDate { get; set; }

        public static Result<Departure> Create(Maybe<int> monkeyId)
        {
            return monkeyId.ToResult("Monkey Id must not be null")
                .Ensure(request => request > 0, "Monkey Id must be valid")
                .Map(request => new Departure(request));
        }
        private Departure(int monkeyId)
        {
            MonkeyId = monkeyId;

            DepartureDate = DateTime.Today;
            
        }
    }
}
