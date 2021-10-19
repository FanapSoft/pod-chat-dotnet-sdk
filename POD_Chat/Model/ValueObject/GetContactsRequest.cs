using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using POD_Async.Base;

namespace POD_Chat.Model.ValueObject
{
    public class GetContactsRequest : ChatMessageContent
    {
        public static Builder ConcreteBuilder => new Builder();

        [JsonProperty("size")]
        public long? Size { get; }

        [JsonProperty("offset")]
        public long Offset { get; }

        [JsonProperty("query")]
        public string Query { get; }

        [JsonProperty("cellphoneNumber")]
        public string CellphoneNumber { get; }

        [JsonProperty("email")]
        public string Email { get; }

        [JsonProperty("id")]
        public long? Id { get; }

        public GetContactsRequest(Builder builder)
        {
            Size = builder.GetSize();
            Offset = builder.GetOffset();
            Query = builder.GetQuery();
            CellphoneNumber = builder.GetCellphoneNumber();
            Email = builder.GetEmail();
            UniqueId = builder.GetUniqueId();
            Id = builder.GetId();
            TypeCode = builder.GetTypeCode();
        }

        public override string GetJsonContent()
        {
            return this.ToJsonWithNotNullProperties();
        }

        public class Builder
        {
            private int size;
            private int offset;
            private string query;
            private string cellphoneNumber;

            [EmailAddress]
            private string email;
            private string uniqueId;
            private long? id;
            private string typeCode;

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

            internal string GetQuery()
            {
                return query;
            }

            public Builder SetQuery(string query)
            {
                this.query = query;
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

            public GetContactsRequest Build()
            {
                return new GetContactsRequest(this);
            }
        }
    }
}
