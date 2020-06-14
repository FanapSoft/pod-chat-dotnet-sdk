using System.ComponentModel.DataAnnotations;
using System.Linq;
using POD_Async.Base;
using POD_Async.Exception;

namespace POD_Chat.Model.ValueObject
{
    public class UploadFileRequest
    {
        public static Builder ConcreteBuilder => new Builder();
        public string FilePath { get; }
        public string FileName { get; }

        public UploadFileRequest(Builder builder)
        {
            FilePath = builder.GetFilePath();
            FileName = builder.GetFileName();
        }
        public class Builder
        {
            [Required]
            private string filePath;
            private string fileName;

            internal string GetFilePath()
            {
                return filePath;
            }

            public Builder SetFilePath(string filePath)
            {
                this.filePath = filePath;
                return this;
            }

            internal string GetFileName()
            {
                return fileName;
            }

            public Builder SetFileName(string fileName)
            {
                this.fileName = fileName;
                return this;
            }

            public UploadFileRequest Build()
            {
                var hasErrorFields = this.ValidateByAttribute();

                if (hasErrorFields.Any())
                {
                    throw PodException.BuildException(new InvalidParameter(hasErrorFields));
                }

                return new UploadFileRequest(this);
            }
        }
    }
}
