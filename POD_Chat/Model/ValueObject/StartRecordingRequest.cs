namespace POD_Chat.Model.ValueObject
{
    public class StartRecordingRequest: ChatMessageContent
    {
        public long CallId { get; set; }

        public StartRecordingRequest(long callId)
        {
            CallId = callId;
        }

        public override string GetJsonContent()
        {
            return null;
        }
    }
}
