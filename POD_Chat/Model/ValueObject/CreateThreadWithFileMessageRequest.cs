using POD_Async.Base;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using POD_Async.Exception;

namespace POD_Chat.Model.ValueObject
{
    public class CreateThreadWithFileMessageRequest
    {
        public static Builder ConcreteBuilder => new Builder();

        public CreateThreadWithMessageRequest CreateThreadWithMessageInput { get; }
        public UploadRequest UploadInput { get; }

        public CreateThreadWithFileMessageRequest(Builder builder)
        {
            CreateThreadWithMessageInput = builder.GetCreateThreadWithMessageInput();
            UploadInput = builder.GetUploadInput();
        }

        public class Builder
        {
            [Required]
            private CreateThreadWithMessageRequest createThreadWithMessageInput;

            [Required]
            private UploadRequest uploadInput;

            public CreateThreadWithMessageRequest GetCreateThreadWithMessageInput()
            {
                return createThreadWithMessageInput;
            }

            public Builder SetCreateThreadWithMessageInput(CreateThreadWithMessageRequest createThreadWithMessageInput)
            {
                this.createThreadWithMessageInput = createThreadWithMessageInput;
                return this;
            }          

            public UploadRequest GetUploadInput()
            {
                return uploadInput;
            }

            public Builder SetUploadInput(UploadRequest uploadInput)
            {
                this.uploadInput = uploadInput;
                return this;
            }
           
            public CreateThreadWithFileMessageRequest Build()
            {
                var hasErrorFields = this.ValidateByAttribute();
               
                if (hasErrorFields.Any())
                {
                    throw PodException.BuildException(new InvalidParameter(hasErrorFields));
                }

                return new CreateThreadWithFileMessageRequest(this);
            }
        }
    }
}
