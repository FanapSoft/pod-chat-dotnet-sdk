using System.ComponentModel.DataAnnotations;
using System.Linq;
using Newtonsoft.Json;
using POD_Async.Base;
using POD_Async.Exception;

namespace POD_Chat.Model.ValueObject
{
    public class RemoveParticipantsRequest : ChatMessageContent
    {
        public static Builder ConcreteBuilder => new Builder();

        [JsonProperty("threadId")]
        public long? ThreadId { get; }

        [JsonProperty("participantIds")]
        public long[] ParticipantIds { get; }

        public RemoveParticipantsRequest(Builder builder)
        {
            ThreadId = builder.GetThreadId();
            ParticipantIds = builder.GetParticipantIds();
            TypeCode = builder.GetTypeCode();
        }

        public override string GetJsonContent()
        {
            return ParticipantIds.ToJson();
        }

        public class Builder
        {
            [Required]
            private long threadId;

            [Required]
            private long[] participantIds;
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

            internal long[] GetParticipantIds()
            {
                return participantIds;
            }

            public Builder SetParticipantIds(long[] participantIds)
            {
                this.participantIds = participantIds;
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

            public RemoveParticipantsRequest Build()
            {
                var hasErrorFields = this.ValidateByAttribute();

                if (hasErrorFields.Any())
                {
                    throw PodException.BuildException(new InvalidParameter(hasErrorFields));
                }

                return new RemoveParticipantsRequest(this);
            }
        }
    }
}
