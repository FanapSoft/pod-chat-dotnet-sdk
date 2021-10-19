using System.ComponentModel.DataAnnotations;
using System.Linq;
using Newtonsoft.Json;
using POD_Async.Base;
using POD_Async.Exception;
using POD_Chat.Base.Enum;

namespace POD_Chat.Model.ValueObject
{
    public class GetHistoryRequest : ChatMessageContent
    {
        public static Builder ConcreteBuilder => new Builder();

        [JsonProperty("threadId")]
        public long? ThreadId { get; }

        [JsonProperty("order")]
        public int? Order { get; }

        [JsonProperty("userId")]
        public long? UserId { get; }

        [JsonProperty("id")]
        public long? Id { get; }

        [JsonProperty("query")]
        public string Query { get; }

        [JsonProperty("fromTime")]
        public long? FromTime { get; }

        [JsonProperty("fromTimeNanos")]
        public long? FromTimeNanos { get; }

        [JsonProperty("toTimeNanos")]
        public long? ToTimeNanos { get; }

        [JsonProperty("toTime")]
        public long? ToTime { get; }

        [JsonProperty("metadataCriteria")]
        public string MetadataCriteria { get; }

        [JsonProperty("firstMessageId")]
        public long? FirstMessageId { get; }

        [JsonProperty("lastMessageId")]
        public long? LastMessageId { get; }

        [JsonProperty("uniqueIds")]
        public string[] UniqueIds { get; }

        [JsonProperty("messageType")]
        public int? MessageType { get; }

        [JsonProperty("unreadMentioned")]
        public bool? UnreadMentioned { get; }

        [JsonProperty("allMentioned")]
        public bool? AllMentioned { get; }

        [JsonProperty("count")]
        public int Count { get; }

        [JsonProperty("offset")]
        public int Offset { get; }

        public GetHistoryRequest(Builder builder)
        {
            ThreadId = builder.GetThreadId();
            Order = builder.GetOrder();
            UserId = builder.GetUserId();
            Id = builder.GetId();
            Query = builder.GetQuery();
            FromTime = builder.GetFromTime();
            ToTime = builder.GetToTime();
            FromTimeNanos = builder.GetFromTimeNanos();
            ToTimeNanos = builder.GetToTimeNanos();
            MetadataCriteria = builder.GetMetadataCriteria();
            FirstMessageId = builder.GetFirstMessageId();
            LastMessageId = builder.GetLastMessageId();
            UniqueIds = builder.GetUniqueIds();
            MessageType = builder.GetMessageType();
            UnreadMentioned = builder.GetUnreadMentioned();
            AllMentioned = builder.GetAllMentioned();
            Count = builder.GetCount();
            Offset = builder.GetOffset();
            UniqueId = builder.GetUniqueId();
            TypeCode = builder.GetTypeCode();
        }

        public override string GetJsonContent()
        {
            return this.ToJsonWithNotNullProperties();
        }

        public class Builder
        {
            [Required]
            private long threadId;
            private int? order;
            private long? userId;
            private long? id;
            private string query;
            private long? fromTime;
            private long? toTime;
            private long? fromTimeNanos;
            private long? toTimeNanos;
            private string metadataCriteria;
            private long? firstMessageId;
            private long? lastMessageId;
            private string[] uniqueIds;
            private int? messageType;
            private bool? unreadMentioned;
            private bool? allMentioned;
            private int count;
            private int offset;
            private string typeCode;
            private string uniqueId;

            internal long? GetThreadId()
            {
                return threadId;
            }

            public Builder SetThreadId(long threadId)
            {
                this.threadId = threadId;
                return this;
            }

            internal long? GetFirstMessageId()
            {
                return firstMessageId > 0 ? firstMessageId : null;
            }

            /// <param name="firstMessageId">جهت تعیین شروع بازه ی تاریخچه ی دریافتی</param>
            public Builder SetFirstMessageId(long firstMessageId)
            {
                this.firstMessageId = firstMessageId;
                return this;
            }

            internal long? GetLastMessageId()
            {
                return lastMessageId > 0 ? lastMessageId : null;
            }

            /// <param name="lastMessageId">جهت تعیین پایان بازه ی تاریخچه ی دریافتی</param>
            public Builder SetLastMessageId(long lastMessageId)
            {
                this.lastMessageId = lastMessageId;
                return this;
            }

            internal long? GetFromTime()
            {
                return fromTime;
            }

            public Builder SetFromTime(long fromTime)
            {
                this.fromTime = fromTime;
                return this;
            }

            internal long? GetFromTimeNanos()
            {
                return fromTimeNanos;
            }

            public Builder SetFromTimeNanos(long fromTimeNanos)
            {
                this.fromTimeNanos = fromTimeNanos;
                return this;
            }

            internal long? GetToTime()
            {
                return toTime;
            }

            public Builder SetToTime(long toTime)
            {
                this.toTime = toTime;
                return this;
            }

            internal long? GetToTimeNanos()
            {
                return toTimeNanos;
            }

            public Builder SetToTimeNanos(long toTimeNanos)
            {
                this.toTimeNanos = toTimeNanos;
                return this;
            }

            internal int? GetOrder()
            {
                return order;
            }

            /// <param name="order">ترتیب پیمایش پیام ها</param>
            public Builder SetOrder(OrderType order)
            {
                this.order = (int)order;
                return this;
            }

            internal long? GetUserId()
            {
                return userId;
            }

            public Builder SetUserId(long userId)
            {
                this.userId = userId;
                return this;
            }

            internal long? GetId()
            {
                return id > 0 ? id : null;
            }

            /// <summary>
            /// با وارد کردن شناسه پیام میتوانید فقط همان پیام را دریافت کنید
            /// </summary>
            public Builder SetId(long id)
            {
                this.id = id;
                return this;
            }

            internal string GetQuery()
            {
                return query;
            }

            /// <param name="query">جستجو در محتوای پیام ها</param>
            public Builder SetQuery(string query)
            {
                this.query = query;
                return this;
            }

            internal string GetMetadataCriteria()
            {
                return metadataCriteria;
            }

            public Builder SetMetadataCriteria(string metadataCriteria)
            {
                this.metadataCriteria = metadataCriteria;
                return this;
            }

            internal string[] GetUniqueIds()
            {
                return uniqueIds;
            }

            public Builder SetUniqueIds(string[] uniqueIds)
            {
                this.uniqueIds = uniqueIds;
                return this;
            }

            internal int? GetMessageType()
            {
                return messageType;
            }

            public Builder SetMessageType(MessageType messageType)
            {
                this.messageType = (int)messageType;
                return this;
            }

            internal bool? GetUnreadMentioned()
            {
                return unreadMentioned;
            }

            public Builder SetUnreadMentioned(bool unreadMentioned)
            {
                this.unreadMentioned = unreadMentioned;
                return this;
            }

            internal bool? GetAllMentioned()
            {
                return allMentioned;
            }

            public Builder SetAllMentioned(bool allMentioned)
            {
                this.allMentioned = allMentioned;
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

            public GetHistoryRequest Build()
            {
                var hasErrorFields = this.ValidateByAttribute();

                if (hasErrorFields.Any())
                {
                    throw PodException.BuildException(new InvalidParameter(hasErrorFields));
                }

                return new GetHistoryRequest(this);
            }
        }
    }
}
