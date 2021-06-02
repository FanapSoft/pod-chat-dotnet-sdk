using Newtonsoft.Json;
using POD_Async.Base;
using POD_Async.Exception;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace POD_Chat.Model.ValueObject
{
    public class RemoveCallParticipantsRequest : ChatMessageContent
    {
        public static Builder ConcreteBuilder => new Builder();

        [JsonProperty("threadId")]
        public long? ThreadId { get; }

        [JsonProperty("coreUserIds")]
        public long[] UserIds { get; }

        public RemoveCallParticipantsRequest(Builder builder)
        {
            ThreadId = builder.GetThreadId();
            UserIds = builder.GetUserIds();
            TypeCode = builder.GetTypeCode();
        }

        public override string GetJsonContent()
        {
            return UserIds.ToJson();
        }

        public class Builder
        {
            [Required]
            private long threadId;

            private long[] userIds;

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

            internal long[] GetUserIds()
            {
                return userIds;
            }

            public Builder SetUserIds(long[] userIds)
            {
                this.userIds = userIds;
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

            public RemoveCallParticipantsRequest Build()
            {
                var hasErrorFields = this.ValidateByAttribute();

                if (hasErrorFields.Any())
                {
                    throw PodException.BuildException(new InvalidParameter(hasErrorFields));
                }

                return new RemoveCallParticipantsRequest(this);
            }
        }
    }
}
