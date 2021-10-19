using System.ComponentModel.DataAnnotations;
using System.Linq;
using Newtonsoft.Json;
using POD_Async.Base;
using POD_Async.Exception;

namespace POD_Chat.Model.ValueObject
{
    public class EditMessageRequest : ChatMessageContent
    {
        public static Builder ConcreteBuilder => new Builder();

        [JsonProperty("textMessage")]
        public string TextMessage { get; }

        [JsonProperty("messageId")]
        public long? MessageId { get; }

        [JsonProperty("systemSystemMetadata")]
        public string SystemMetadata { get; }

        public EditMessageRequest(Builder builder)
        {
            TextMessage = builder.GetTextMessage();
            MessageId = builder.GetMessageId();
            SystemMetadata = builder.GetSystemMetadata();
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
            private long? messageId;
            private string systemMetadata;
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

            internal long? GetMessageId()
            {
                return messageId;
            }

            public Builder SetMessageId(long messageId)
            {
                this.messageId = messageId;
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

            public EditMessageRequest Build()
            {
                var hasErrorFields = this.ValidateByAttribute();

                if (hasErrorFields.Any())
                {
                    throw PodException.BuildException(new InvalidParameter(hasErrorFields));
                }

                return new EditMessageRequest(this);
            }
        }
    }
}
