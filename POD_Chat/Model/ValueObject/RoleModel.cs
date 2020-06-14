using System.ComponentModel.DataAnnotations;
using System.Linq;
using Newtonsoft.Json;
using POD_Async.Base;
using POD_Async.Exception;
using POD_Chat.Base.Enum;

namespace POD_Chat.Model.ValueObject
{
    public class RoleModel
    {
        public static Builder ConcreteBuilder => new Builder();

        [JsonProperty("roles")]
        public string[] Roles { get; }

        [JsonProperty("userId")]
        public long? UserId { get; }

        public RoleModel(Builder builder)
        {
            Roles = builder.GetRoles();
            UserId = builder.GetUserId();
        }

        public class Builder
        {
            [Required]
            private string[] roles;

            [Required]
            private long? userId;

            internal string[] GetRoles()
            {
                return roles;
            }

            public Builder SetRoles(RoleType[] roles)
            {
                this.roles = roles.Select(_=>_.ToString()).ToArray();
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

            public RoleModel Build()
            {
                var hasErrorFields = this.ValidateByAttribute();

                if (hasErrorFields.Any())
                {
                    throw PodException.BuildException(new InvalidParameter(hasErrorFields));
                }

                return new RoleModel(this);
            }
        }
    }
}
