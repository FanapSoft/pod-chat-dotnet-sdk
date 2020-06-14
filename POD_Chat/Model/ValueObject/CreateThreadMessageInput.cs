using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Newtonsoft.Json;
using POD_Async.Base;
using POD_Async.CustomAttribute;
using POD_Async.Exception;
using POD_Chat.Base.Enum;

namespace POD_Chat.Model.ValueObject
{
    public class CreateThreadMessageInput
    {
        public static Builder ConcreteBuilder => new Builder();

        [JsonProperty("text")]
        public string Text { get; }

        [JsonProperty("systemMetadata")]
        public string SystemMetadata { get; }

        [JsonProperty("metadata")]
        public string Metadata { get; }

        [JsonProperty("forwardedMessageIds")]
        public long[] ForwardedMessageIds { get; }

        [JsonProperty("forwardedUniqueIds")]
        internal string[] ForwardedUniqueIds { get; }

        [JsonProperty("uniqueId")]
        internal string UniqueId { get; }

        [JsonProperty("messageType")]
        public int? MessageType { get; }

        public CreateThreadMessageInput(Builder builder)
        {
            Text = builder.GetText();
            SystemMetadata = builder.GetSystemMetadata();
            Metadata = builder.GetMetadata();
            ForwardedMessageIds = builder.GetForwardedMessageIds();
            ForwardedUniqueIds = builder.GetForwardedUniqueIds();
            UniqueId = builder.GetUniqueId();
            MessageType = builder.GetMessageType();
        }

        public class Builder
        {
            [RequiredIf(nameof(forwardedMessageIds))]
            private string text;
            private string systemMetadata;
            private string metadata;
            private long[] forwardedMessageIds;
            private string[] forwardedUniqueIds;
            private string uniqueId;

            [Required]
            private int? messageType;

            internal string GetText()
            {
                return text;
            }

            public Builder SetText(string text)
            {
                this.text = text;
                return this;
            }

            internal string GetUniqueId()
            {
                if (!string.IsNullOrEmpty(text))
                {
                    uniqueId = Guid.NewGuid().ToString();
                }

                return uniqueId;
            }

            internal string GetSystemMetadata()
            {
                return systemMetadata;
            }

            public Builder SetSystemMetadata(string systemMetadata)
            {
                this.systemMetadata = systemMetadata;
                return this;
            }

            internal string GetMetadata()
            {
                return metadata;
            }

            public Builder SetMetadata(string metadata)
            {
                this.metadata = metadata;
                return this;
            }

            internal long[] GetForwardedMessageIds()
            {
                return forwardedMessageIds;
            }

            public Builder SetForwardedMessageIds(long[] forwardedMessageIds)
            {
                this.forwardedMessageIds = forwardedMessageIds;
                return this;
            }

            internal string[] GetForwardedUniqueIds()
            {
                if (forwardedMessageIds != null)
                {
                    forwardedUniqueIds = new string[forwardedMessageIds.Length];
                    for (var i = 0; i < forwardedMessageIds.Length; i++)
                    {
                        forwardedUniqueIds[i] = Guid.NewGuid().ToString();
                    }
                }

                return forwardedUniqueIds;
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

            public CreateThreadMessageInput Build()
            {
                var hasErrorFields = this.ValidateByAttribute();

                if (hasErrorFields.Any())
                {
                    throw PodException.BuildException(new InvalidParameter(hasErrorFields));
                }

                return new CreateThreadMessageInput(this);
            }
        }
    }
}
