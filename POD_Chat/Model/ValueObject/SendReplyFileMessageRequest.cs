using System.ComponentModel.DataAnnotations;
using System.Linq;
using Newtonsoft.Json;
using POD_Async.Base;
using POD_Async.Exception;

namespace POD_Chat.Model.ValueObject
{
    public class SendReplyFileMessageRequest : ChatMessageContent
    {
        public static Builder ConcreteBuilder => new Builder();

        public long? RepliedTo { get; }
        public SendFileMessageRequest SendFileMessage { get; }

        public SendReplyFileMessageRequest(Builder builder)
        {
            RepliedTo = builder.GetRepliedTo();
            SendFileMessage = builder.GetSendFileMessage();
        }

        public override string GetJsonContent()
        {
            return this.ToJsonWithNotNullProperties();
        }

        public class Builder
        {
            [Required]
            private long? repliedTo;

            [Required]
            private SendFileMessageRequest sendFileMessage;

            internal long? GetRepliedTo()
            {
                return repliedTo;
            }

            public Builder SetRepliedTo(long repliedTo)
            {
                this.repliedTo = repliedTo;
                return this;
            }

            internal SendFileMessageRequest GetSendFileMessage()
            {
                return sendFileMessage;
            }

            public Builder SetSendFileMessage(SendFileMessageRequest sendFileMessage)
            {
                this.sendFileMessage = sendFileMessage;
                return this;
            }

            public SendReplyFileMessageRequest Build()
            {
                var hasErrorFields = this.ValidateByAttribute();

                if (hasErrorFields.Any())
                {
                    throw PodException.BuildException(new InvalidParameter(hasErrorFields));
                }

                return new SendReplyFileMessageRequest(this);
            }
        }
    }
}
