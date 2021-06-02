using Newtonsoft.Json;
using POD_Async.Base;
using POD_Chat.Base.Enum;
using System.Collections.Generic;

namespace POD_Chat.Model.ValueObject
{
    public class CallRequest : ChatMessageContent
    {

        public static Builder ConcreteBuilder(SendClientDTO sendClientDTO)
        {
            return new Builder(sendClientDTO);
        }

        [JsonProperty("threadId")]
        public long? ThreadId { get; }

        [JsonProperty("invitees")]
        public List<InviteVo> Invitees { get; }

        [JsonProperty("type")]
        public CallType Type { get; }


        [JsonProperty("creatorClientDto")]
        public SendClientDTO CreatorClientDto { get; }

        public CallRequest(Builder builder)
        {
            ThreadId = builder.GetThreadId();
            Invitees = builder.GetInvitees();
            Type = builder.GetCallType();
            CreatorClientDto = builder.GetCreateClientDTO();
        }

        public override string GetJsonContent()
        {
            return this.ToJsonWithNotNullProperties();
        }

        public class Builder
        {
            private List<InviteVo> Invitees;
            private long? ThreadId;
            private CallType Type = CallType.VOICE_CALL;
            private SendClientDTO SendClientDTO;

            public Builder(SendClientDTO sendClientDTO) {
                SendClientDTO = sendClientDTO;
            }

            internal long? GetThreadId()
            {
                return ThreadId;
            }

            internal CallType GetCallType()
            {
                return Type;
            }

            internal List<InviteVo> GetInvitees()
            {
                return Invitees;
            }

            internal SendClientDTO GetCreateClientDTO() {
                return SendClientDTO;
            }

            public Builder SetInvitees(List<InviteVo> invitees)
            {
                this.Invitees = invitees;
                return this;
            }

            public Builder SetThreadId(long threadId)
            {
                this.ThreadId = threadId;
                return this;
            }

            public Builder SetCallType(CallType type)
            {
                this.Type = type;
                return this;
            }

            public CallRequest Build()
            {
                return new CallRequest(this);
            }
        }

        
    }
}
