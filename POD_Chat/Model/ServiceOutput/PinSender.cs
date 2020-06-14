
namespace POD_Chat.Model.ServiceOutput
{
    public class PinSender
    {
        public long Id { get; set; }
        public long CoreUserId { get; set; }
        public long NotSeenDuration { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
    }
}
