using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using POD_Async.Base;
using POD_Async.CustomAttribute;
using POD_Async.Exception;

namespace POD_Chat.Model.ValueObject
{
    public class BlockRequest : ChatMessageContent
    {
        public static Builder ConcreteBuilder => new Builder();

        [JsonProperty("threadId")]
        public long? ThreadId { get; }

        [JsonProperty("userId")]
        public long? UserId { get; }

        [JsonProperty("contactId")]
        public long? ContactId { get; }

        public BlockRequest(Builder builder)
        {
            ThreadId = builder.GetThreadId();
            UserId = builder.GetUserId();
            ContactId = builder.GetContactId();
            TypeCode = builder.GetTypeCode();
            UniqueId = builder.GetUniqueId();
        }

        public override string GetJsonContent()
        {
            //var jObject = new JObject
            //{
            //    {"contactId", ContactId},
            //    {"userId", UserId},
            //    {"threadId", ThreadId},
            //};

            return this.ToJsonWithNotNullProperties();
        }

        public class Builder
        {
            [RequiredIf(nameof(userId), nameof(contactId))]
            private long? threadId;
            private long? userId;
            private long? contactId;
            private string typeCode;
            private string uniqueId;

            internal long? GetThreadId()
            {
                return threadId;
            }

            public Builder SetThreadId(long threadId)
            {
                this.threadId = threadId;
                return this;
            }

            internal long? GetUserId()
            {
                return userId;
            }

            public Builder SetUserId(long userId)
            {
                this.userId = userId;
                return this;
            }

            internal long? GetContactId()
            {
                return contactId;
            }

            public Builder SetContactId(long contactId)
            {
                this.contactId = contactId;
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

            public BlockRequest Build()
            {
                var hasErrorFields = this.ValidateByAttribute();

                if (hasErrorFields.Any())
                {
                    throw PodException.BuildException(new InvalidParameter(hasErrorFields));
                }

                return new BlockRequest(this);
            }
        }
    }
}
