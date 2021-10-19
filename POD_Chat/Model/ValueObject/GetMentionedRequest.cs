using System.ComponentModel.DataAnnotations;
using System.Linq;
using Newtonsoft.Json;
using POD_Async.Base;
using POD_Async.Exception;


namespace POD_Chat.Model.ValueObject
{
    public class GetMentionedRequest : ChatMessageContent
    {
        public static Builder ConcreteBuilder => new Builder();

        [JsonProperty("threadId")]
        public long? ThreadId { get; }

        [JsonProperty("unreadMentioned")]
        public bool? UnreadMentioned { get; }

        [JsonProperty("allMentioned")]
        public bool? AllMentioned { get; }

        [JsonProperty("count")]
        public int Count { get; }

        [JsonProperty("offset")]
        public int Offset { get; }

        public GetMentionedRequest(Builder builder)
        {
            ThreadId = builder.GetThreadId();
            UnreadMentioned = builder.GetUnreadMentioned();
            AllMentioned = builder.GetAllMentioned();
            Count = builder.GetCount();
            Offset = builder.GetOffset();
            TypeCode = builder.GetTypeCode();
            UniqueId = builder.GetUniqueId();
        }

        public override string GetJsonContent()
        {
            return this.ToJson();
        }

        public class Builder
        {
            [Required]
            private long? threadId;
            private bool? unreadMentioned;
            private bool? allMentioned;
            //onlyUnreadMention:  Bool
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

            public GetMentionedRequest Build()
            {
                var hasErrorFields = this.ValidateByAttribute();

                if (hasErrorFields.Any())
                {
                    throw PodException.BuildException(new InvalidParameter(hasErrorFields));
                }

                return new GetMentionedRequest(this);
            }
        }
    }
}
