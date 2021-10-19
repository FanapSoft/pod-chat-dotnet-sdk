using POD_Async.Base;

namespace POD_Chat.Model.ValueObject
{
    public class MuteUnMuteCallParticipantsRequest:ChatMessageContent
    {
        public long CallId { get; set; }
        public long[] UserIds { get; set; }

        public MuteUnMuteCallParticipantsRequest(long callId , long[] userIds, string uniqueId = null,string typeCode = null)
        {
            CallId = callId;
            UserIds = userIds;
            UniqueId = uniqueId;
            TypeCode = typeCode;
        }

        public override string GetJsonContent()
        {
            return UserIds.ToJson();
        }
    }
}
