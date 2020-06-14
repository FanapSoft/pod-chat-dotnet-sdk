using System.Collections.Generic;
using Newtonsoft.Json;
using POD_Async.Base;

namespace POD_Chat.Model.ValueObject
{
    public class GetThreadsRequest : ChatMessageContent
    {
        public static Builder ConcreteBuilder => new Builder();

        [JsonProperty("threadIds")]
        public List<long> ThreadIds { get; }

        [JsonProperty("name")]
        public string ThreadName { get; }

        [JsonProperty("creatorCoreUserId")]
        public long? CreatorCoreUserId { get; }

        [JsonProperty("partnerCoreUserId")]
        public long? PartnerCoreUserId { get; }

        [JsonProperty("partnerCoreContactId")]
        public long? PartnerCoreContactId { get; }

        [JsonProperty("new")]
        public bool? IsNew { get; }

        [JsonProperty("count")]
        public int Count { get; }

        [JsonProperty("offset")]
        public int Offset { get; }

        public GetThreadsRequest(Builder builder)
        {
            ThreadIds = builder.GetThreadIds();
            ThreadName = builder.GetThreadName();
            CreatorCoreUserId = builder.GetCreatorCoreUserId();
            PartnerCoreUserId = builder.GetPartnerCoreUserId();
            PartnerCoreContactId = builder.GetPartnerCoreContactId();
            Count = builder.GetCount();
            Offset = builder.GetOffset();
            TypeCode = builder.GetTypeCode();
        }

        public override string GetJsonContent()
        {
            return this.ToJsonWithNotNullProperties();
        }

        public class Builder
        {
            private List<long> threadIds;
            private string threadName;
            private long? creatorCoreUserId;
            private long? partnerCoreUserId;
            private long? partnerCoreContactId;
            private bool? isNew;
            private int count;
            private int offset;
            private string typeCode;

            internal List<long> GetThreadIds()
            {
                return threadIds;
            }

            public Builder SetThreadIds(List<long> threadIds)
            {
                this.threadIds = threadIds;
                return this;
            }

            internal string GetThreadName()
            {
                return threadName;
            }

            public Builder SetThreadName(string threadName)
            {
                this.threadName = threadName;
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

            internal long? GetPartnerCoreUserId()
            {
                return partnerCoreUserId;
            }

            public Builder SetPartnerCoreUserId(long partnerCoreUserId)
            {
                this.partnerCoreUserId = partnerCoreUserId;
                return this;
            }

            internal long? GetPartnerCoreContactId()
            {
                return partnerCoreContactId;
            }

            public Builder SetPartnerCoreContactId(long partnerCoreContactId)
            {
                this.partnerCoreContactId = partnerCoreContactId;
                return this;
            }

            internal bool? GetIsNew()
            {
                return isNew;
            }

            public Builder SetIsNew(bool isNew)
            {
                this.isNew = isNew;
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

            public GetThreadsRequest Build()
            {
                return new GetThreadsRequest(this);
            }
        }
    }
}
