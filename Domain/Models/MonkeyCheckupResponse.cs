
namespace Domain.Models
{
    public class MonkeyCheckupResponse : MonkeyInfo
    {
        public override int MonkeyId { get; set; }
        public override string MonkeyName { get; set; }

        public override DateTime CheckupTime { get; set; }
    }
}
