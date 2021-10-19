using System.ComponentModel.DataAnnotations;
using System.Linq;
using Newtonsoft.Json;
using POD_Async.Base;
using POD_Async.Exception;

namespace POD_Chat.Model.ValueObject
{
    public class SendDeliverSeenRequest : ChatMessageContent
    {
        public static Builder ConcreteBuilder => new Builder();

        [JsonProperty("messageId")]
        public long? MessageId { get; }

        [JsonProperty("ownerId")]
        public long? OwnerId { get; }

        public SendDeliverSeenRequest(Builder builder)
        {
            MessageId = builder.GetMessageId();
            OwnerId = builder.GetOwnerId();
            TypeCode = builder.GetTypeCode();
            UniqueId = builder.GetUniqueId();
        }

        public override string GetJsonContent()
        {
            return MessageId.ToString();
        }

        public class Builder
        {
            [Required]
            private long? messageId;
            private long? ownerId;
            private string typeCode;
            private string uniqueId;

            internal long? GetMessageId()
            {
                return messageId;
            }

            public Builder SetMessageId(long messageId)
            {
                this.messageId = messageId;
                return this;
            }

            internal long? GetOwnerId()
            {
                return ownerId;
            }

            public Builder SetOwnerId(long ownerId)
            {
                this.ownerId = ownerId;
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

            public SendDeliverSeenRequest Build()
            {
                var hasErrorFields = this.ValidateByAttribute();

                if (hasErrorFields.Any())
                {
                    throw PodException.BuildException(new InvalidParameter(hasErrorFields));
                }

                return new SendDeliverSeenRequest(this);
            }
        }
    }
}
