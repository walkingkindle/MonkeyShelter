using CSharpFunctionalExtensions;

namespace Domain.Entities
{
    public class Admission
    {
        public int Id { get; set; }

        public int MonkeyId { get; set; }

        public DateTime MonkeyAdmittanceDate { get; set; }

        public DateTime MonkeyCheckupTime { get; set; }


        public static Result<Admission> CreateAdmission(Maybe<int> monkeyId, Maybe<DateTime> admittanceDate)
        {
            return monkeyId.ToResult("Admission cannot be null")
                .Ensure(request => request >= 0, "Admittance entry must have a valid monkey Id")
                .Ensure(request => admittanceDate.HasValue, "Admittance date must not be null")
                .Ensure(request => admittanceDate.Value <= DateTime.Today, "Monkey admittance date must be valid")
                .Map(request => new Admission(monkeyId.Value,admittanceDate.Value));
        }

        private Admission(int monkeyId, DateTime monkeyAdmittanceDate)
        {
            MonkeyId = monkeyId;

            MonkeyAdmittanceDate = monkeyAdmittanceDate;

            MonkeyCheckupTime = CalculateCheckupTime(monkeyAdmittanceDate);
        }

        private DateTime CalculateCheckupTime(DateTime monkeyAdmittanceDate)
        {
            return monkeyAdmittanceDate.AddMonths(6);
        }
    }
}
