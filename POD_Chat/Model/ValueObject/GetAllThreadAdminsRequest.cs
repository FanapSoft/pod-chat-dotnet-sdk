using System.ComponentModel.DataAnnotations;
using System.Linq;
using Newtonsoft.Json;
using POD_Async.Base;
using POD_Async.Exception;

namespace POD_Chat.Model.ValueObject
{
    public class GetAllThreadAdminsRequest : ChatMessageContent
    {
        public static Builder ConcreteBuilder => new Builder();

        [JsonProperty("threadId")]
        public long? ThreadId { get; }

        [JsonProperty("size")]
        public int Count { get; }

        [JsonProperty("offset")]
        public int Offset { get; }

        public GetAllThreadAdminsRequest(Builder builder)
        {
            ThreadId = builder.GetThreadId();
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
            [Required]
            private long? threadId;
            private int count;
            private int offset;
            private string typeCode;

            public long? GetThreadId()
            {
                return threadId;
            }

            public Builder SetThreadId(long threadId)
            {
                this.threadId = threadId;
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

            internal string GetTypeCode()
            {
                return typeCode;
            }

            public Builder SetTypeCode(string typeCode)
            {
                this.typeCode = typeCode;
                return this;
            }

            public GetAllThreadAdminsRequest Build()
            {
                var hasErrorFields = this.ValidateByAttribute();

                if (hasErrorFields.Any())
                {
                    throw PodException.BuildException(new InvalidParameter(hasErrorFields));
                }

                return new GetAllThreadAdminsRequest(this);
            }
        }
    }
}
