using System.ComponentModel.DataAnnotations;
using System.Linq;
using POD_Async.Base;
using POD_Async.Exception;

namespace POD_Chat.Model.ValueObject
{
    public class DownloadPodSpaceFileRequest
    {
        public static Builder ConcreteBuilder => new Builder();

        public string Hash { get; }
        public string DownloadPath { get; }
        public string FileName { get; }

        public DownloadPodSpaceFileRequest(Builder builder)
        {
            Hash = builder.GetHash();
            DownloadPath = builder.GetDownloadPath();
            FileName = builder.GetFileName();
        }

        public class Builder
        {
            [Required]
            private string hash;

            [Required]
            private string downloadPath;

            [Required]
            private string fileName;

            internal string GetHash()
            {
                return hash;
            }

            public Builder SetHash(string hash)
            {
                this.hash = hash;
                return this;
            }

            internal string GetDownloadPath()
            {
                return downloadPath;
            }

            public Builder SetDownloadPath(string downloadPath)
            {
                this.downloadPath = downloadPath;
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

            public DownloadPodSpaceFileRequest Build()
            {
                var hasErrorFields = this.ValidateByAttribute();

                if (hasErrorFields.Any())
                {
                    throw PodException.BuildException(new InvalidParameter(hasErrorFields));
                }

                return new DownloadPodSpaceFileRequest(this);
            }
        }
    }
}
