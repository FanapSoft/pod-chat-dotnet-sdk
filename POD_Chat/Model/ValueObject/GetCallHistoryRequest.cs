using Newtonsoft.Json;
using POD_Async.Base;
using POD_Async.Exception;
using POD_Chat.Base.Enum;
using System.Linq;

namespace POD_Chat.Model.ValueObject
{
    public class GetCallHistoryRequest : ChatMessageContent
    {
        public static Builder ConcreteBuilder => new Builder();

        [JsonProperty("count")]
        public int Count { get; }

        [JsonProperty("offset")]
        public int Offset { get; }

        [JsonProperty("callIds")]
        public long[] CallIds { get; }

        [JsonProperty("type")]
        public CallType? Type { get; }

        [JsonProperty("name")]
        public string Name { get; }

        [JsonProperty("creatorCoreUserId")]
        public long? CreatorCoreUserId { get; }

        [JsonProperty("creatorSsoId")]
        public long? CreatorSsoId { get; }

        public GetCallHistoryRequest(Builder builder)
        {
            Count = builder.GetCount();
            Offset = builder.GetOffset();
            TypeCode = builder.GetTypeCode();
            UniqueId = builder.GetUniqueId();
            CallIds = builder.GetCallIds();
            Type = builder.GetCallType();
            Name = builder.GetName();
            CreatorCoreUserId = builder.GetCreatorCoreUserId();
            CreatorSsoId = builder.GetCreatorSSOId();
        }

        public override string GetJsonContent()
        {
            return this.ToJsonWithNotNullProperties();
        }

        public class Builder
        {
            private int count;
            private int offset;
            private long[] callIds;
            private CallType? type;
            private string name;
            private long? creatorCoreUserId;
            private long? creatorSsoId;
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

            internal long[] GetCallIds() {
                return callIds;
            }

            public Builder SetCallIds(long[] callIds)
            {
                this.callIds = callIds;
                return this;
            }

            internal CallType? GetCallType()
            {
                return type;
            }

            public Builder SetCallType(CallType callType)
            {
                this.type = callType;
                return this;
            }

            internal string GetName()
            {
                return name;
            }

            public Builder SetName(string name)
            {
                this.name = name;
                return this;
            }

            internal long? GetCreatorCoreUserId()
            {
                return creatorCoreUserId;
            }

            public Builder SetCreatorCoreUserId(long creatorCoreUserId)
            {
                this.creatorCoreUserId = creatorCoreUserId;
                return this;
            }

            internal long? GetCreatorSSOId()
            {
                return creatorSsoId;
            }

            public Builder SetGetCreatorSSOId(long creatorSsoId)
            {
                this.creatorSsoId = creatorSsoId;
                return this;
            }

            public GetCallHistoryRequest Build()
            {
                var hasErrorFields = this.ValidateByAttribute();

                if (hasErrorFields.Any())
                {
                    throw PodException.BuildException(new InvalidParameter(hasErrorFields));
                }

                return new GetCallHistoryRequest(this);
            }
        }
    }
}
