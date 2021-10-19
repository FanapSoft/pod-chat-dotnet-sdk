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
            UniqueId = builder.GetUniqueId();
        }

        public override string GetJsonContent()
        {
            return null;
        }

        public class Builder
        {
            private long callId;
            private string typeCode;
            private string uniqueId;

            public Builder(long callId ) {
                this.callId = callId;
            }

            internal long GetCallId()
            {
                return callId;
            }

            public string GetUniqueId()
            {
                return uniqueId;
            }

            public Builder SetUniqueId(string uniqueId)
            {
                this.uniqueId = uniqueId;
                return this;
            }

            public string GetTypeCode()
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
