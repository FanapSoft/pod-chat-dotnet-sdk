namespace POD_Chat.Model.ValueObject
{
    public class TurnOffVideoCallRequest
    {
        public long CallId { get; set; }

        public TurnOffVideoCallRequest(long callId)
        {
            CallId = callId;
        }

    }
}
