using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Newtonsoft.Json;
using POD_Async.Base;
using POD_Async.CustomAttribute;
using POD_Async.Exception;
using POD_Chat.Base.Enum;

namespace POD_Chat.Model.ValueObject
{
    public class RemoveParticipantsRequest : ChatMessageContent
    {
        public static Builder ConcreteBuilder => new Builder();

        [JsonProperty("threadId")]
        public long? ThreadId { get; }

        [JsonProperty("participantIds")]
        public long[] ParticipantIds { get; }

        [JsonProperty("coreUserIds")]
        public List<InviteVo> CoreUserIds { get; }

        public RemoveParticipantsRequest(Builder builder)
        {
            ThreadId = builder.GetThreadId();
            ParticipantIds = builder.GetParticipantIds();
            CoreUserIds = builder.GetCoreUserIds();
            TypeCode = builder.GetTypeCode();
        }

        public override string GetJsonContent()
        {
            if (ParticipantIds != null && ParticipantIds.Length > 0)
                return ParticipantIds.ToJson();
            return CoreUserIds.ToJson();
        }

        public class Builder
        {
            [Required]
            private long threadId;

            [RequiredIf(nameof(coreUserIds))]
            private long[] participantIds;

            private List<InviteVo> coreUserIds;
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

            internal List<InviteVo> GetCoreUserIds()
            {
                return coreUserIds;
            }

            public Builder SetCoreUserIds(long[] coreUserIds)
            {
                var invitees = new List<InviteVo>();
                for (int i = 0; i < coreUserIds.Length; i++)
                {
                    invitees.Add(InviteVo.ConcreteBuilder.SetId(coreUserIds[i].ToString()).SetIdType(InviteType.TO_BE_CORE_USER_ID).Build());
                }

                this.coreUserIds = invitees;
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
