
namespace POD_Chat.Model.ServiceOutput
{
    public class PinUnpinMessageModel
    {
        public long MessageId { get; set; }
        public string Text { get; set; }
        public bool NotifyAll { get; set; }
        public long Time { get; set; }
        public Participant Sender { get; set; }
        //public PinSender Sender { get; set; }
    }
}
