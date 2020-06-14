using System.ComponentModel.DataAnnotations;
using System.Linq;
using POD_Async.Base;
using POD_Async.Exception;

namespace POD_Chat.Model.ValueObject
{
    public class SendFileMessageRequest
    {
        public static Builder ConcreteBuilder => new Builder();

        public SendTextMessageRequest MessageInput { get; }
        public UploadRequest UploadInput { get; }

        public SendFileMessageRequest(Builder builder)
        {
            UploadInput = builder.GetUploadInput();
            MessageInput = builder.GetMessageInput();
            UploadInput.UserGroupHash = builder.GetUserGroupHash();
        }

        public class Builder
        {
            [Required]
            private UploadRequest uploadInput;

            [Required]
            private SendTextMessageRequest messageInput;

            [Required]
            private string userGroupHash;

            internal UploadRequest GetUploadInput()
            {
                return uploadInput;
            }

            public Builder SetUploadInput(UploadRequest uploadInput)
            {
                this.uploadInput = uploadInput;
                return this;
            }

            internal SendTextMessageRequest GetMessageInput()
            {
                return messageInput;
            }

            public Builder SetMessageInput(SendTextMessageRequest messageInput)
            {
                this.messageInput = messageInput;
                return this;
            }

            internal string GetUserGroupHash()
            {
                return userGroupHash;
            }

            /// <param name="userGroupHash">Get this value from output of CreateThread</param>
            public Builder SetUserGroupHash(string userGroupHash)
            {
                this.userGroupHash = userGroupHash;
                return this;
            }

            public SendFileMessageRequest Build()
            {
                var hasErrorFields = this.ValidateByAttribute();

                if (hasErrorFields.Any())
                {
                    throw PodException.BuildException(new InvalidParameter(hasErrorFields));
                }

                return new SendFileMessageRequest(this);
            }
        }
    }
}
