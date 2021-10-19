namespace POD_Chat.Model.ValueObject
{
    public class TurnOffVideoCallRequest:ChatMessageContent
    {
        public long CallId { get; set; }

        public TurnOffVideoCallRequest(long callId,string uniqueId = null , string typeCode = null)
        {
            CallId = callId;
            UniqueId = uniqueId;
            TypeCode = typeCode;
        }

        public override string GetJsonContent()
        {
            return null;
        }
    }
}
