using POD_Async.Base;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Newtonsoft.Json;
using POD_Async.Exception;
using Newtonsoft.Json.Linq;

namespace POD_Chat.Model.ValueObject
{
    public class CreateThreadWithMessageRequest : ChatMessageContent
    {
        public static Builder ConcreteBuilder => new Builder();

        public CreateThreadMessageInput MessageInput { get; }
        public CreateThreadRequest CreateThreadInput { get; }

        public CreateThreadWithMessageRequest(Builder builder)
        {
            CreateThreadInput = builder.GetCreateThreadInput();
            MessageInput = builder.GetMessageInput();
        }

        public override string GetJsonContent()
        {
            var threadRequestContent = CreateThreadInput.GetJsonContent();
            var jObject = JObject.Parse(threadRequestContent);
            jObject.Add("message", MessageInput.ToJsonWithNotNullProperties());
            return jObject.ToString();
        }

        public class Builder
        {
            [Required]
            private CreateThreadRequest createThreadInput;

            [Required]
            private CreateThreadMessageInput messageInput;


            internal CreateThreadRequest GetCreateThreadInput()
            {
                return createThreadInput;
            }

            public Builder SetCreateThreadInput(CreateThreadRequest createThreadInput)
            {
                this.createThreadInput = createThreadInput;
                return this;
            }

            internal CreateThreadMessageInput GetMessageInput()
            {
                return messageInput;
            }

            public Builder SetMessageInput(CreateThreadMessageInput messageInput)
            {
                this.messageInput = messageInput;
                return this;
            }

            public CreateThreadWithMessageRequest Build()
            {
                var hasErrorFields = this.ValidateByAttribute();

                if (hasErrorFields.Any())
                {
                    throw PodException.BuildException(new InvalidParameter(hasErrorFields));
                }

                return new CreateThreadWithMessageRequest(this);
            }
        }
    }
}
