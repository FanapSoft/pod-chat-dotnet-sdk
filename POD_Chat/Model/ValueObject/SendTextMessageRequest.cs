using System.ComponentModel.DataAnnotations;
using System.Linq;
using Newtonsoft.Json;
using POD_Async.Base;
using POD_Async.Exception;
using POD_Chat.Base.Enum;

namespace POD_Chat.Model.ValueObject
{
    public class SendTextMessageRequest : ChatMessageContent
    {
        public static Builder ConcreteBuilder => new Builder();

        [JsonProperty("content")]
        public string TextMessage { get; internal set; }

        [JsonProperty("messageType")]
        public int? MessageType { get; set; }

        [JsonProperty("systemMetadata")]
        public string SystemMetadata { get; set; }

        [JsonProperty("metadata")]
        public string Metadata { get; internal set; }

        [JsonProperty("subjectId")]
        public long? ThreadId { get; }

        public SendTextMessageRequest(Builder builder)
        {
            TextMessage = builder.GetTextMessage();
            MessageType = builder.GetMessageType();
            SystemMetadata = builder.GetSystemMetadata();
            Metadata = builder.GetMetadata();
            ThreadId = builder.GetThreadId();
            TypeCode = builder.GetTypeCode();
            UniqueId = builder.GetUniqueId();
        }

        public override string GetJsonContent()
        {
            return TextMessage;
        }

        public class Builder
        {
            [Required]
            private string textMessage;

            [Required]
            private int? messageType;
            private string systemMetadata;
            private string metadata;

            [Required]
            private long? threadId;
            private string typeCode;
            private string uniqueId;

            internal string GetTextMessage()
            {
                return textMessage;
            }

            public Builder SetTextMessage(string textMessage)
            {
                this.textMessage = textMessage;
                return this;
            }

            internal int? GetMessageType()
            {
                return messageType;
            }

            public Builder SetMessageType(MessageType messageType)
            {
                this.messageType = (int)messageType;
                return this;
            }

            internal string GetSystemMetadata()
            {
                return systemMetadata;
            }

            public Builder SetSystemMetadata(string systemMetadata)
            {
                this.systemMetadata = systemMetadata;
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
            internal long? GetThreadId()
            {
                return threadId;
            }

            public Builder SetThreadId(long threadId)
            {
                this.threadId = threadId;
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

            public SendTextMessageRequest Build()
            {
                var hasErrorFields = this.ValidateByAttribute();

                if (hasErrorFields.Any())
                {
                    throw PodException.BuildException(new InvalidParameter(hasErrorFields));
                }

                return new SendTextMessageRequest(this);
            }
        }
    }
}
