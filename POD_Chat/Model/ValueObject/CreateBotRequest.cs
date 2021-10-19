using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Newtonsoft.Json;
using POD_Async.Base;
using POD_Async.Exception;

namespace POD_Chat.Model.ValueObject
{
    public class CreateBotRequest : ChatMessageContent
    {
        public static Builder ConcreteBuilder => new Builder();

        [JsonProperty("botName")]
        public string BotName { get; }

        public CreateBotRequest(Builder builder)
        {
            BotName = builder.GetBotName();
            TypeCode = builder.GetTypeCode();
            UniqueId = builder.GetUniqueId();
        }

        public override string GetJsonContent()
        {
            return BotName;
        }

        public class Builder
        {
            [Required]
            private string botName;
            private string typeCode;
            private string uniqueId;

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

            public CreateBotRequest Build()
            {
                var hasErrorFields = this.ValidateByAttribute();
                if (hasErrorFields.Any())
                {
                    throw PodException.BuildException(new InvalidParameter(hasErrorFields));
                }

                return new CreateBotRequest(this);
            }
        }
    }
}
