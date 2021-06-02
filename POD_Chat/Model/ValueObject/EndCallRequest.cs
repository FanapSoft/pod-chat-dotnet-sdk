using Newtonsoft.Json;
using POD_Async.Base;

namespace POD_Chat.Model.ValueObject
{
    public class EndCallRequest : ChatMessageContent
    {
        public long CallId { get; }


        public static Builder ConcreteBuilder(long callId)
        {
            return new Builder(callId);
        }

        [JsonProperty("creatorClientDto")]
        public SendClientDTO CreatorClientDto { get; }

        public EndCallRequest(Builder builder)
        {
            CallId = builder.GetCallId();
            TypeCode = builder.GetTypeCode();
        }

        public override string GetJsonContent()
        {
            return null;
        }

        public class Builder
        {
            private long callId;
            private string typeCode;

            public Builder(long callId ) {
                this.callId = callId;
            }

            internal long GetCallId()
            {
                return callId;
            }

            internal string GetTypeCode()
            {
                return typeCode;
            }

            public Builder SetTypeCode(string typeCode)
            {
                this.typeCode = typeCode;
                return this;
            }


            public EndCallRequest Build()
            {
                return new EndCallRequest(this);
            }
        }

        
    }
}
