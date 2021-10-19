using Newtonsoft.Json;
using POD_Async.Base;

namespace POD_Chat.Model.ValueObject
{
    public class UpdateChatProfileRequest : ChatMessageContent
    {
        public static Builder ConcreteBuilder => new Builder();

        [JsonProperty("bio")]
        public string Bio { get; }

        [JsonProperty("metadata")]
        public string Metadata { get; }

        public UpdateChatProfileRequest(Builder builder)
        {
            Bio = builder.GetBio();
            Metadata = builder.GetMetadata();
            TypeCode = builder.GetTypeCode();
            UniqueId = builder.GetUniqueId();
        }

        public override string GetJsonContent()
        {
            return this.ToJson();
        }

        public class Builder
        {
            private string bio;
            private string metadata;
            private string typeCode;
            private string uniqueId;

            internal string GetBio()
            {
                return bio;
            }

            public Builder SetBio(string bio)
            {
                this.bio = bio;
                return this;
            }

            internal string GetMetadata()
            {
                return metadata;
            }

            public Builder SetMetadata(string metadata)
            {
                this.metadata = metadata;
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

            public UpdateChatProfileRequest Build()
            {
                return new UpdateChatProfileRequest(this);
            }
        }
    }
}
