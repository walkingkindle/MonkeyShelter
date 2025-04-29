
namespace Domain.Models
{
    public class MonkeyCheckupResponse : MonkeyInfo
    {
        public override int Id { get; set; }
        public override string Name { get; set; }

        public override DateTime CheckupTime { get; set; }
    }
}
