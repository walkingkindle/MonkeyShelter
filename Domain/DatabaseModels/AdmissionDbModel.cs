namespace Domain.DatabaseModels
{
    public class AdmissionDbModel
    {
        public int Id { get; set; }

        public int MonkeyId { get; set; }

        public MonkeyDbModel Monkey { get; set; }

        public DateTime MonkeyAdmittanceDate { get; set; }

        public DateTime MonkeyCheckupTime { get; set; }

        public AdmissionDbModel(int monkeyId,DateTime monkeyAdmittanceDate)
        {
            MonkeyId = monkeyId;
            MonkeyAdmittanceDate = monkeyAdmittanceDate;
            MonkeyCheckupTime = MonkeyAdmittanceDate.AddDays(60); // 60 days as per requirement
        }
    }
}
