using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using POD_Async.Base;
using POD_Async.Exception;

namespace POD_Chat.Model.ValueObject
{
    public class AddContactsRequest
    {
        public static Builder ConcreteBuilder => new Builder();
        public List<AddContactRequest> AddContactList { get; }

        public AddContactsRequest(Builder builder)
        {
            AddContactList = builder.GetAddContactList();
        }

        public class Builder
        {
            [Required]
            private List<AddContactRequest> addContactList;

            internal List<AddContactRequest> GetAddContactList()
            {
                return addContactList;
            }

            public Builder SetAddContactList(List<AddContactRequest> addContactList)
            {
                this.addContactList = addContactList;
                return this;
            }

            public AddContactsRequest Build()
            {
                var hasErrorFields = this.ValidateByAttribute();

                if (hasErrorFields.Any())
                {
                    throw PodException.BuildException(new InvalidParameter(hasErrorFields));
                }

                return new AddContactsRequest(this);
            }
        }
    }
}
