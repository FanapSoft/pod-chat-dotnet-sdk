using Newtonsoft.Json;
using POD_Async.Base;

namespace POD_Chat.Model.ValueObject
{
    public class GetBlockedListRequest : ChatMessageContent
    {
        public static Builder ConcreteBuilder => new Builder();

        [JsonProperty("size")]
        public int Count { get; }

        [JsonProperty("offset")]
        public int Offset { get; }

        public GetBlockedListRequest(Builder builder)
        {
            Count = builder.GetCount();
            Offset = builder.GetOffset();
            TypeCode = builder.GetTypeCode();
            UniqueId = builder.GetUniqueId();
        }

        public override string GetJsonContent()
        {
            return this.ToJsonWithNotNullProperties();
        }

        public class Builder
        {
            private int count;
            private int offset;
            private string typeCode;
            private string uniqueId;

            internal int GetCount()
            {
                return count > 0 ? count : 5;
            }

            public Builder SetCount(int count)
            {
                this.count = count;
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

            public GetBlockedListRequest Build()
            {
                return new GetBlockedListRequest(this);
            }
        }
    }
}
