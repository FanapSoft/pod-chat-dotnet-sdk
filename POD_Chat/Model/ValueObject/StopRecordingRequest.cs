using POD_Async.Base;

namespace POD_Chat.Model.ValueObject
{
    public class StopRecordingRequest : ChatMessageContent
    {
        public long CallId { get; set; }
        public long[] UserIds { get; set; }

        public StopRecordingRequest(long callId , long[] userIds)
        {
            CallId = callId;
            UserIds = userIds;
        }

        public override string GetJsonContent()
        {
            return UserIds.ToJson();
        }
    }
}
