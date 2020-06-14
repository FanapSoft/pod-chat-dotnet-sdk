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
        }

        public override string GetJsonContent()
        {
            return this.ToJson();
        }

        public class Builder
        {
            private string bio;
            private string metadata;

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

            public UpdateChatProfileRequest Build()
            {
                return new UpdateChatProfileRequest(this);
            }
        }
    }
}
