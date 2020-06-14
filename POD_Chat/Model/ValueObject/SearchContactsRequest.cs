using System.ComponentModel.DataAnnotations;
using System.Linq;
using POD_Async.Base;
using POD_Async.Exception;

namespace POD_Chat.Model.ValueObject
{
    public class SearchContactsRequest
    {
        public static Builder ConcreteBuilder => new Builder();
        public string FirstName { get; }
        public string LastName { get; }
        public string CellphoneNumber { get; }
        public string Email { get; }
        public string UniqueId { get; }
        public string Username { get; }
        public string Query { get; }
        public long? Id { get; }
        public string TypeCode { get; }
        public string OwnerId { get; }
        public int Offset { get; }
        public int Size { get; }

        public SearchContactsRequest(Builder builder)
        {
            FirstName = builder.GetFirstName();
            LastName = builder.GetLastName();
            CellphoneNumber = builder.GetCellphoneNumber();
            Email = builder.GetEmail();
            UniqueId = builder.GetUniqueId();
            Username = builder.GetUsername();
            Query = builder.GetQuery();
            Id = builder.GetId();
            TypeCode = builder.GetTypeCode();
            OwnerId = builder.GetOwnerId();
            Offset = builder.GetOffset();
            Size = builder.GetSize();
        }

        public class Builder
        {
            private string firstName;
            private string lastName;
            private string cellphoneNumber;

            [EmailAddress]
            private string email;
            private string uniqueId;
            private string username;
            private string query;
            private long? id;
            private string typeCode;
            private string ownerId;
            private int offset;
            private int size;

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

            internal string GetUsername()
            {
                return username;
            }

            public Builder SetUsername(string username)
            {
                this.username = username;
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

            internal string GetQuery()
            {
                return query;
            }

            public Builder SetQuery(string query)
            {
                this.query = query;
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

            internal string GetOwnerId()
            {
                return ownerId;
            }

            public Builder SetOwnerId(string ownerId)
            {
                this.ownerId = ownerId;
                return this;
            }

            internal int GetSize()
            {
                return size > 0 ? size : 5;
            }

            public Builder SetSize(int size)
            {
                this.size = size;
                return this;
            }

            internal int GetOffset()
            {
                return offset > 0 ? offset : 0;
            }

            public Builder SetOffset(int offset)
            {
                this.offset = offset;
                return this;
            }

            public SearchContactsRequest Build()
            {
                var hasErrorFields = this.ValidateByAttribute();

                if (hasErrorFields.Any())
                {
                    throw PodException.BuildException(new InvalidParameter(hasErrorFields));
                }

                return new SearchContactsRequest(this);
            }
        }
    }
}
