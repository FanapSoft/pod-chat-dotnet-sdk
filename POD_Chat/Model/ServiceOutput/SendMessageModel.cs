
namespace POD_Chat.Model.ServiceOutput
{
    public class SendMessageModel
    {
        public long? ThreadId { get; set; }
        public MessageVO Message { get; set; }
        public long? ParticipantId { get; set; }
        public long? MessageId { get; set; }
        public long? ConversationId { get; set; }
    }
}
