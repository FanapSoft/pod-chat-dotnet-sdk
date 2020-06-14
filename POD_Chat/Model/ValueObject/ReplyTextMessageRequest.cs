using System.ComponentModel.DataAnnotations;
using System.Linq;
using Newtonsoft.Json;
using POD_Async.Base;
using POD_Async.Exception;

namespace POD_Chat.Model.ValueObject
{
    public class ReplyTextMessageRequest
    {
        public static Builder ConcreteBuilder => new Builder();

        public SendTextMessageRequest TextMessage { get; }
        public long? RepliedTo { get; }

        public ReplyTextMessageRequest(Builder builder)
        {
            TextMessage = builder.GetTextMessage();
            RepliedTo = builder.GetRepliedTo();
        }

        public class Builder
        {
            [Required]
            private SendTextMessageRequest textMessage;

            [Required]
            private long? repliedTo;

            internal SendTextMessageRequest GetTextMessage()
            {
                return textMessage;
            }

            public Builder SetTextMessageRequest(SendTextMessageRequest textMessage)
            {
                this.textMessage = textMessage;
                return this;
            }

            internal long? GetRepliedTo()
            {
                return repliedTo;
            }

            public Builder SetRepliedTo(long repliedTo)
            {
                this.repliedTo = repliedTo;
                return this;
            }

            public ReplyTextMessageRequest Build()
            {
                var hasErrorFields = this.ValidateByAttribute();

                if (hasErrorFields.Any())
                {
                    throw PodException.BuildException(new InvalidParameter(hasErrorFields));
                }

                return new ReplyTextMessageRequest(this);
            }
        }
    }
}
