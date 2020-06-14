using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using POD_Async.Base;
using POD_Async.Exception;

namespace POD_Chat.Model.ValueObject
{
    public class DeleteMultipleMessagesRequest : ChatMessageContent
    {
        public static Builder ConcreteBuilder => new Builder();

        [JsonProperty("deleteForAll")]
        public bool DeleteForAll { get; }

        [JsonProperty("messageIds")]
        public long[] MessageIds { get; }

        public DeleteMultipleMessagesRequest(Builder builder)
        {
            DeleteForAll = builder.GetDeleteForAll();
            MessageIds = builder.GetMessageIds();
            TypeCode = builder.GetTypeCode();
        }

        public override string GetJsonContent()
        {
            var multipleDeleteUniqueIds = new string[MessageIds.Length];
            for (var i = 0; i < MessageIds.Length; i++)
            {
                multipleDeleteUniqueIds[i] = Guid.NewGuid().ToString();
            }

            var jObject = new JObject
            {
                {"ids", MessageIds.ToJson()},
                {"uniqueIds", multipleDeleteUniqueIds.ToJson()},
                {"deleteForAll", DeleteForAll},
            };

            return jObject.ToString();
        }

        public class Builder
        {
            private bool deleteForAll;

            [Required]
            private long[] messageIds;
            private string typeCode;

            internal bool GetDeleteForAll()
            {
                return deleteForAll;
            }

            public Builder SetDeleteForAll(bool deleteForAll)
            {
                this.deleteForAll = deleteForAll;
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

            internal string GetTypeCode()
            {
                return typeCode;
            }

            public Builder SetTypeCode(string typeCode)
            {
                this.typeCode = typeCode;
                return this;
            }

            public DeleteMultipleMessagesRequest Build()
            {
                var hasErrorFields = this.ValidateByAttribute();

                if (hasErrorFields.Any())
                {
                    throw PodException.BuildException(new InvalidParameter(hasErrorFields));
                }

                return new DeleteMultipleMessagesRequest(this);
            }
        }
    }
}
