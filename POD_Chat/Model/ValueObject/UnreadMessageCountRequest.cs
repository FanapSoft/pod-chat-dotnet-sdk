using Newtonsoft.Json;
using POD_Async.Base;

namespace POD_Chat.Model.ValueObject
{
    public class UnreadMessageCountRequest : ChatMessageContent
    {
        public static Builder ConcreteBuilder => new Builder();

        [JsonProperty("mute")]
        public bool? Mute { get; }

        public UnreadMessageCountRequest(Builder builder)
        {
            Mute = builder.GetMute();
            TypeCode = builder.GetTypeCode();
            UniqueId = builder.GetUniqueId();
        }

        public override string GetJsonContent()
        {
            return this.ToJsonWithNotNullProperties();
        }

        public class Builder
        {
            private bool? mute;
            private string typeCode;
            private string uniqueId;

            internal bool? GetMute()
            {
                return mute;
            }

            public Builder SetMute(bool mute)
            {
                this.mute = mute;
                return this;
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

            public UnreadMessageCountRequest Build()
            {
                return new UnreadMessageCountRequest(this);
            }
        }
    }
}
