namespace POD_Chat.Model.ValueObject
{
    public class TurnOnVideoCallRequest
    {
        public long CallId { get; set; }

        public TurnOnVideoCallRequest(long callId) {
            CallId = callId;
        }
    }
}
