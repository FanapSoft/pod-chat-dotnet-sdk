using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Newtonsoft.Json;
using POD_Async.Base;
using POD_Async.Exception;

namespace POD_Chat.Model.ValueObject
{
    public class DefineBotCommandRequest : ChatMessageContent
    {
        public static Builder ConcreteBuilder => new Builder();

        [JsonProperty("botName")]
        public string BotName { get; }

        [JsonProperty("commandList")]
        public string[] CommandList { get; }
        public DefineBotCommandRequest(Builder builder)
        {
            BotName = builder.GetBotName();
            CommandList = builder.GetCommandList();
            TypeCode = builder.GetTypeCode();
        }

        public override string GetJsonContent()
        {
            return this.ToJson();
        }

        public class Builder
        {
            [Required]
            private string botName;

            [Required]
            private string[] commandList;
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

            internal string[] GetCommandList()
            {
                return commandList;
            }

            public Builder SetCommandList(string[] commandList)
            {
                for (var i = 0; i < commandList.Length; i++)
                {
                    if (!commandList[i].StartsWith("/"))
                        commandList[i] = "/" + commandList[i];

                    if (commandList[i].Contains("@"))
                        commandList[i] = commandList[i].Replace("@", "");
                }

                this.commandList = commandList;
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

            public DefineBotCommandRequest Build()
            {
                var hasErrorFields = this.ValidateByAttribute();
                if (hasErrorFields.Any())
                {
                    throw PodException.BuildException(new InvalidParameter(hasErrorFields));
                }

                return new DefineBotCommandRequest(this);
            }
        }
    }
}
