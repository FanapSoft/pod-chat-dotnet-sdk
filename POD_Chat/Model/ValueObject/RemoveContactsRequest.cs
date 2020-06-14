using System.ComponentModel.DataAnnotations;
using System.Linq;
using POD_Async.Base;
using POD_Async.Exception;

namespace POD_Chat.Model.ValueObject
{
    public class RemoveContactsRequest
    {
        public static Builder ConcreteBuilder => new Builder();
        public long? Id { get; }

        public RemoveContactsRequest(Builder builder)
        {
            Id = builder.GetId();
        }

        public class Builder
        {
            [Required]
            private long? id;

            internal long? GetId()
            {
                return id;
            }

            public Builder SetId(long id)
            {
                this.id = id;
                return this;
            }

            public RemoveContactsRequest Build()
            {
                var hasErrorFields = this.ValidateByAttribute();

                if (hasErrorFields.Any())
                {
                    throw PodException.BuildException(new InvalidParameter(hasErrorFields));
                }

                return new RemoveContactsRequest(this);
            }
        }
    }
}
