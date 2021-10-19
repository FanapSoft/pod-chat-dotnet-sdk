using System.ComponentModel.DataAnnotations;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using POD_Async.Base;
using POD_Async.Exception;

namespace POD_Chat.Model.ValueObject
{
    public class DeleteMessageRequest : ChatMessageContent
    {
        public static Builder ConcreteBuilder => new Builder();

        [JsonProperty("deleteForAll")]
        public bool DeleteForAll { get; }

        [JsonProperty("messageId")]
        public long? MessageId { get; }

        public DeleteMessageRequest(Builder builder)
        {
            DeleteForAll = builder.GetDeleteForAll();
            MessageId = builder.GetMessageId();
            TypeCode = builder.GetTypeCode();
            UniqueId = builder.GetUniqueId();
        }

        public override string GetJsonContent()
        {
            var jObject = new JObject { { "deleteForAll", DeleteForAll } };
            return jObject.ToString();
        }

        public class Builder
        {
            private bool deleteForAll;

            [Required]
            private long? messageId;
            private string typeCode;
            private string uniqueId;

            internal bool GetDeleteForAll()
            {
                return deleteForAll;
            }

            public Builder SetDeleteForAll(bool deleteForAll)
            {
                this.deleteForAll = deleteForAll;
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

            public DeleteMessageRequest Build()
            {
                var hasErrorFields = this.ValidateByAttribute();

                if (hasErrorFields.Any())
                {
                    throw PodException.BuildException(new InvalidParameter(hasErrorFields));
                }

                return new DeleteMessageRequest(this);
            }
        }
    }
}
