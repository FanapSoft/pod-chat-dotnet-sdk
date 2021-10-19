using System.ComponentModel.DataAnnotations;
using System.Linq;
using Newtonsoft.Json;
using POD_Async.Base;
using POD_Async.Exception;

namespace POD_Chat.Model.ValueObject
{
    public class GetThreadParticipantsRequest : ChatMessageContent
    {
        public static Builder ConcreteBuilder => new Builder();

        [JsonProperty("threadId")]
        public long? ThreadId { get; }

        [JsonProperty("name")]
        public string Name { get; }

        [JsonProperty("admin")]
        internal bool? Admin { get; set; }

        [JsonProperty("count")]
        public int Count { get; }

        [JsonProperty("offset")]
        public int Offset { get; }

        public GetThreadParticipantsRequest(Builder builder)
        {
            ThreadId = builder.GetThreadId();
            Name = builder.GetName();
            Admin = builder.GetAdmin();
            Count = builder.GetCount();
            Offset = builder.GetOffset();
            TypeCode = builder.GetTypeCode();
            UniqueId = builder.GetUniqueId();
        }

        public override string GetJsonContent()
        {
            var contentJson = new
            {
                name = Name,
                count = Count,
                offset = Offset,
                admin = Admin
            };

            return contentJson.ToJsonWithNotNullProperties();
        }

        public class Builder
        {
            [Required]
            private long? threadId;
            private string name;
            private bool? admin;
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

            internal string GetName()
            {
                return name;
            }

            public Builder SetName(string name)
            {
                this.name = name;
                return this;
            }

            internal bool? GetAdmin()
            {
                return admin;
            }

            internal Builder SetAdmin(bool admin)
            {
                this.admin = admin;
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

            public GetThreadParticipantsRequest Build()
            {
                var hasErrorFields = this.ValidateByAttribute();

                if (hasErrorFields.Any())
                {
                    throw PodException.BuildException(new InvalidParameter(hasErrorFields));
                }

                return new GetThreadParticipantsRequest(this);
            }
        }
    }
}
