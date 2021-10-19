using System.ComponentModel.DataAnnotations;
using System.Linq;
using POD_Async.Base;
using POD_Async.Exception;

namespace POD_Chat.Model.ValueObject
{
    public class AddContactRequest
    {
        public static Builder ConcreteBuilder => new Builder();
        public string FirstName { get; }
        public string LastName { get; }
        public string CellphoneNumber { get; }
        public string Email { get; }
        public string UniqueId { get; }
        public string TypeCode { get; }
        public string Username { get; }

        public AddContactRequest(Builder builder)
        {
            FirstName = builder.GetFirstName();
            LastName = builder.GetLastName();
            CellphoneNumber = builder.GetCellphoneNumber();
            Email = builder.GetEmail();
            UniqueId = builder.GetUniqueId();
            TypeCode = builder.GetTypeCode();
            Username = builder.GetUsername();
        }
        
        public class Builder
        {
            private string firstName;
            private string lastName;
            private string cellphoneNumber;

            [EmailAddress]
            private string email;
            private string uniqueId;
            private string typeCode;

            [Required]
            private string username;

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

            public string GetUniqueId()
            {
                return uniqueId;
            }

            public Builder SetUniqueId(string uniqueId)
            {
                this.uniqueId = uniqueId;
                return this;
            }

            public string GetTypeCode()
            {
                return typeCode;
            }

            public Builder SetTypeCode(string typeCode)
            {
                this.typeCode = typeCode;
                return this;
            }

            internal string GetUsername()
            {
                return username;
            }

            public Builder SetUsername(string username)
            {
                this.username = username;
                return this;
            }

            public AddContactRequest Build()
            {
                var hasErrorFields = this.ValidateByAttribute();

                if (hasErrorFields.Any())
                {
                    throw PodException.BuildException(new InvalidParameter(hasErrorFields));
                }

                return new AddContactRequest(this);
            }
        }
    }
}
