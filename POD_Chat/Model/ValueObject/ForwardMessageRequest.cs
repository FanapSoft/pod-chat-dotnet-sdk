using System.ComponentModel.DataAnnotations;
using System.Linq;
using Newtonsoft.Json;
using POD_Async.Base;
using POD_Async.Exception;

namespace POD_Chat.Model.ValueObject
{
    public class ForwardMessageRequest : ChatMessageContent
    {
        public static Builder ConcreteBuilder => new Builder();

        [JsonProperty("threadId")]
        public long? ThreadId { get; }

        [JsonProperty("messageIds")]
        public long[] MessageIds { get; }

        [JsonProperty("metadata")]
        public string Metadata { get; }

        public ForwardMessageRequest(Builder builder)
        {
            ThreadId = builder.GetThreadId();
            MessageIds = builder.GetMessageIds();
            Metadata = builder.GetMetadata();
            TypeCode = builder.GetTypeCode();
        }

        public override string GetJsonContent()
        {
            return MessageIds.ToJson();
        }

        public class Builder
        {
            [Required]
            private long threadId;

            [Required]
            private long[] messageIds;
            private string metadata;
            private string typeCode;

            internal long? GetThreadId()
            {
                return threadId;
            }

            public Builder SetThreadId(long threadId)
            {
                this.threadId = threadId;
                return this;
            }

            internal long[] GetMessageIds()
            {
                return messageIds;
            }

            public Builder SetMessageIds(long[] messageIds)
            {
                this.messageIds = messageIds;
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

            internal string GetTypeCode()
            {
                return typeCode;
            }

            public Builder SetTypeCode(string typeCode)
            {
                this.typeCode = typeCode;
                return this;
            }

            public ForwardMessageRequest Build()
            {
                var hasErrorFields = this.ValidateByAttribute();

                if (hasErrorFields.Any())
                {
                    throw PodException.BuildException(new InvalidParameter(hasErrorFields));
                }

                return new ForwardMessageRequest(this);
            }
        }
    }
}
