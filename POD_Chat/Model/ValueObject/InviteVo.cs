using POD_Async.Base;
using POD_Chat.Base.Enum;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using POD_Async.Exception;
using Newtonsoft.Json;

namespace POD_Chat.Model.ValueObject
{
    public class InviteVo
    {
        public static Builder ConcreteBuilder => new Builder();

        [JsonProperty("id")]
        public string Id { get; }

        [JsonProperty("idType")]
        public int? IdType { get; }

        public InviteVo(Builder builder)
        {
            Id = builder.GetId();
            IdType = builder.GetIdType();
        }

        public class Builder
        {
            [Required]
            private string id;

            [Required]
            private int? idType;

            internal string GetId()
            {
                return id;
            }

            public Builder SetId(string id)
            {
                this.id = id;
                return this;
            }

            internal int? GetIdType()
            {
                return idType;
            }

            public Builder SetIdType(InviteType idType)
            {
                this.idType = (int)idType;
                return this;
            }

            public InviteVo Build()
            {
                var hasErrorFields = this.ValidateByAttribute();

                if (hasErrorFields.Any())
                {
                    throw PodException.BuildException(new InvalidParameter(hasErrorFields));
                }

                return new InviteVo(this);
            }
        }
    }
}
