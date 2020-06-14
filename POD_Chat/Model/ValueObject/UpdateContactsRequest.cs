using System.ComponentModel.DataAnnotations;
using System.Linq;
using POD_Async.Base;
using POD_Async.Exception;

namespace POD_Chat.Model.ValueObject
{
    public class UpdateContactsRequest
    {
        public static Builder ConcreteBuilder => new Builder();
        public string FirstName { get; }
        public string LastName { get; }
        public string CellphoneNumber { get; }
        public string Email { get; }
        public string UniqueId { get; }
        public long? Id { get; }
        public string TypeCode { get; }

        public UpdateContactsRequest(Builder builder)
        {
            FirstName = builder.GetFirstName();
            LastName = builder.GetLastName();
            CellphoneNumber = builder.GetCellphoneNumber();
            Email = builder.GetEmail();
            UniqueId = builder.GetUniqueId();
            Id = builder.GetId();
            TypeCode = builder.GetTypeCode();
        }
        
        public class Builder
        {
            [Required]
            private string firstName;

            [Required]
            private string lastName;

            [Required]
            private string cellphoneNumber;

            [Required]
            [EmailAddress]
            private string email;

            [Required]
            private string uniqueId;

            [Required]
            private long? id;
            private string typeCode;

            internal string GetFirstName()
            {
                return firstName;
            }

            public Builder SetFirstName(string firstName)
            {
                this.firstName = firstName;
                return this;
            }

            internal string GetLastName()
            {
                return lastName;
            }

            public Builder SetLastName(string lastName)
            {
                this.lastName = lastName;
                return this;
            }

            internal string GetCellphoneNumber()
            {
                return cellphoneNumber;
            }

            public Builder SetCellphoneNumber(string cellphoneNumber)
            {
                this.cellphoneNumber = cellphoneNumber;
                return this;
            }

            internal string GetEmail()
            {
                return email;
            }

            public Builder SetEmail(string email)
            {
                this.email = email;
                return this;
            }

            internal string GetUniqueId()
            {
                return uniqueId;
            }

            public Builder SetUniqueId(string uniqueId)
            {
                this.uniqueId = uniqueId;
                return this;
            }

            internal long? GetId()
            {
                return id;
            }

            public Builder SetId(long id)
            {
                this.id = id;
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


            public UpdateContactsRequest Build()
            {
                var hasErrorFields = this.ValidateByAttribute();

                if (hasErrorFields.Any())
                {
                    throw PodException.BuildException(new InvalidParameter(hasErrorFields));
                }

                return new UpdateContactsRequest(this);
            }
        }
    }
}
