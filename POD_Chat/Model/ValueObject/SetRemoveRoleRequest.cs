using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Newtonsoft.Json;
using POD_Async.Base;
using POD_Async.Exception;

namespace POD_Chat.Model.ValueObject
{
    public class SetRemoveRoleRequest : ChatMessageContent
    {
        public static Builder ConcreteBuilder => new Builder();

        [JsonProperty("roles")]
        public List<RoleModel> Roles { get; }

        [JsonProperty("threadId")]
        public long? ThreadId { get; }

        public SetRemoveRoleRequest(Builder builder)
        {
            Roles = builder.GetRoles();
            ThreadId = builder.GetThreadId();
        }

        public override string GetJsonContent()
        {
            return Roles.ToJson();
        }

        public class Builder
        {
            [Required]
            private List<RoleModel> roles;

            [Required]
            private long? threadId;

            internal List<RoleModel> GetRoles()
            {
                return roles;
            }

            public Builder SetRoles(List<RoleModel> roles)
            {
                this.roles = roles;
                return this;
            }

            internal long? GetThreadId()
            {
                return threadId;
            }

            public Builder SetThreadId(long threadId)
            {
                this.threadId = threadId;
                return this;
            }

            public SetRemoveRoleRequest Build()
            {
                var hasErrorFields = this.ValidateByAttribute();

                if (hasErrorFields.Any())
                {
                    throw PodException.BuildException(new InvalidParameter(hasErrorFields));
                }

                return new SetRemoveRoleRequest(this);
            }
        }
    }
}
