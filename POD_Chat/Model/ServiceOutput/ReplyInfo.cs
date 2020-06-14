namespace POD_Chat.Model.ServiceOutput
{
    public class ReplyInfoVO
    {
        public Participant Participant { get; set; }
        public long RepliedToMessageId { get; set; }
        public long RepliedToMessageTime { get; set; }
        public long RepliedToMessageNanos { get; set; }
        public string RepliedToMessage { get; set; }
        public long MessageType { get; set; }
        public bool Deleted { get; set; }
        public string SystemMetadata { get; set; }
        public string Metadata { get; set; }
        public string Message { get; set; }
        public string Time { get; set; }
    }
}
