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

    public class AddParticipantsRequest : ChatMessageContent
    {
        public static Builder ConcreteBuilder => new Builder();

        [JsonProperty("threadId")]
        public long? ThreadId { get; }

        [JsonProperty("contactIds")]
        public long[] ContactIds { get; }

        [JsonProperty("userNames")]
        public List<InviteVo> UserNames { get; }

        [JsonProperty("coreUserIds")]
        public List<InviteVo> CoreUserIds { get; }

        public AddParticipantsRequest(Builder builder)
        {
            ThreadId = builder.GetThreadId();
            ContactIds = builder.GetContactIds();
            UserNames = builder.GetUserNames();
            CoreUserIds = builder.GetCoreUserIds();
            TypeCode = builder.GetTypeCode();
        }

        public override string GetJsonContent()
        {
            if (ContactIds != null && ContactIds.Length > 0)
                return ContactIds.ToJson();
            if (UserNames != null && UserNames.Any())
                return UserNames.ToJson();
            return CoreUserIds.ToJson();
        }

        public class Builder
        {
            [Required]
            private long? threadId;

            [RequiredIf(nameof(userNames), nameof(coreUserIds))]
            private long[] contactIds;

            private List<InviteVo> userNames;
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

            internal long[] GetContactIds()
            {
                return contactIds;
            }

            public Builder SetContactIds(long[] contactIds)
            {
                this.contactIds = contactIds;
                return this;
            }

            internal List<InviteVo> GetUserNames()
            {
                return userNames;
            }

            public Builder SetUserNames(string[] userNames)
            {
                var invitees=new List<InviteVo>();
                for (int i = 0; i < userNames.Length; i++)
                {
                    invitees.Add(InviteVo.ConcreteBuilder.SetId(userNames[i]).SetIdType(InviteType.TO_BE_USER_USERNAME).Build());
                }

                this.userNames = invitees;
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

            public AddParticipantsRequest Build()
            {
                var hasErrorFields = this.ValidateByAttribute();

                if (hasErrorFields.Any())
                {
                    throw PodException.BuildException(new InvalidParameter(hasErrorFields));
                }

                return new AddParticipantsRequest(this);
            }
        }
    }
}
