using System.ComponentModel.DataAnnotations;
using System.Linq;
using Newtonsoft.Json;
using POD_Async.Base;
using POD_Async.Exception;

namespace POD_Chat.Model.ValueObject
{
    public class PinUnpinMessageRequest : ChatMessageContent
    {
        public static Builder ConcreteBuilder => new Builder();

        [JsonProperty("notifyAll")]
        public bool? NotifyAll { get; }

        [JsonProperty("messageId")]
        public long? MessageId { get; }

        public PinUnpinMessageRequest(Builder builder)
        {
            NotifyAll = builder.GetNotifyAll();
            MessageId = builder.GetMessageId();
            TypeCode = builder.GetTypeCode();
            UniqueId = builder.GetUniqueId();
        }

        public override string GetJsonContent()
        {
            var contentJson = new
            {
                notifyAll = NotifyAll,
            };

            return contentJson.ToJson();
        }

        public class Builder
        {
            private bool? notifyAll;

            [Required]
            private long? messageId;
            private string typeCode;
            private string uniqueId;

            internal bool? GetNotifyAll()
            {
                return notifyAll;
            }

            public Builder SetNotifyAll(bool notifyAll)
            {
                this.notifyAll = notifyAll;
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

            public PinUnpinMessageRequest Build()
            {
                var hasErrorFields = this.ValidateByAttribute();

                if (hasErrorFields.Any())
                {
                    throw PodException.BuildException(new InvalidParameter(hasErrorFields));
                }

                return new PinUnpinMessageRequest(this);
            }
        }
    }
}
