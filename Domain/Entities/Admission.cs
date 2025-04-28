using CSharpFunctionalExtensions;
using Domain.Models;

namespace Domain.Entities
{
    public class Admission
    {
        public int Id { get; set; }

        public int MonkeyId { get; set; }

        public DateTime MonkeyAdmittanceDate { get; set; }

        public Monkey Monkey { get; set; }

        public DateTime MonkeyCheckupTime { get; set; }


        public static Result<Admission> CreateAdmission(Maybe<AdmissionRequest> request)
        {
            return request.ToResult("Admission cannot be null")
                .Ensure(request => request.MonkeyId >= 0, "Admittance entry must have a valid monkey Id")
                .Ensure(request => request.AdmittanceDate <= DateTime.Today, "Monkey admittance date must be valid")
                .Map(request => new Admission(request.MonkeyId, request.AdmittanceDate));
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
