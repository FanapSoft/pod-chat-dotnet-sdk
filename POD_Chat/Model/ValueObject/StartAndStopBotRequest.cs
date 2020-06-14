using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Newtonsoft.Json;
using POD_Async.Base;
using POD_Async.Exception;

namespace POD_Chat.Model.ValueObject
{
    public class StartAndStopBotRequest : ChatMessageContent
    {
        public static Builder ConcreteBuilder => new Builder();

        [JsonProperty("botName")]
        public string BotName { get; }

        [JsonProperty("threadId")]
        public long? ThreadId { get; }

        public StartAndStopBotRequest(Builder builder)
        {
            BotName = builder.GetBotName();
            ThreadId = builder.GetThreadId();
            TypeCode = builder.GetTypeCode();
        }

        public override string GetJsonContent()
        {
            var obj = new { botName = BotName };
            return obj.ToJson();
        }

        public class Builder
        {
            [Required]
            private string botName;

            [Required]
            private long? threadId;
            private string typeCode;

            internal string GetBotName()
            {
                return botName;
            }

            public Builder SetBotName(string botName)
            {
                if (!botName.EndsWith("bot", StringComparison.OrdinalIgnoreCase))
                    botName += "BOT";
                this.botName = botName;
                return this;
            }

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

            public StartAndStopBotRequest Build()
            {
                var hasErrorFields = this.ValidateByAttribute();

                if (hasErrorFields.Any())
                {
                    throw PodException.BuildException(new InvalidParameter(hasErrorFields));
                }

                return new StartAndStopBotRequest(this);
            }
        }
    }
}
