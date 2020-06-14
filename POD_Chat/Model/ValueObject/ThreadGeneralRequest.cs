using System.ComponentModel.DataAnnotations;
using System.Linq;
using Newtonsoft.Json;
using POD_Async.Base;
using POD_Async.Exception;

namespace POD_Chat.Model.ValueObject
{
    public class ThreadGeneralRequest : ChatMessageContent
    {
        public static Builder ConcreteBuilder => new Builder();

        [JsonProperty("threadId")]
        public long? ThreadId { get; }

        public ThreadGeneralRequest(Builder builder)
        {
            ThreadId = builder.GetThreadId();
            TypeCode = builder.GetTypeCode();
        }

        public override string GetJsonContent()
        {
            return null;
        }

        public class Builder
        {
            [Required]
            private long? threadId;
            private string typeCode;

            internal long? GetThreadId()
            {
                return threadId;
            }

            public Builder SetThreadId(long threadId)
            {
                this.threadId = threadId;
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

            public ThreadGeneralRequest Build()
            {
                var hasErrorFields = this.ValidateByAttribute();

                if (hasErrorFields.Any())
                {
                    throw PodException.BuildException(new InvalidParameter(hasErrorFields));
                }

                return new ThreadGeneralRequest(this);
            }
        }
    }
}
