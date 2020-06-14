using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using POD_Async.Base;
using POD_Async.Exception;

namespace POD_Chat.Model.ValueObject
{
    public class DownloadPodSpaceImageRequest
    {
        public static Builder ConcreteBuilder => new Builder();

        public string Hash { get; }
        public long? Size { get; }
        public float? Quality { get; }
        public bool? Crop { get; }
        public string DownloadPath { get; }
        public string FileName { get; }

        public DownloadPodSpaceImageRequest(Builder builder)
        {
            Hash = builder.GetHash();
            Size = builder.GetSize();
            Quality = builder.GetQuality();
            Crop = builder.GetCrop();
            DownloadPath = builder.GetDownloadPath();
            FileName = builder.GetFileName();
        }

        public class Builder
        {
            [Required]
            private string hash;
            private long? size;
            private float? quality;
            private bool? crop;

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

            internal long? GetSize()
            {
                return size;
            }

            public Builder SetSize(long size)
            {
                this.size = size;
                return this;
            }

            internal float? GetQuality()
            {
                return quality;
            }

            public Builder SetQuality(float quality)
            {
                this.quality = quality;
                return this;
            }

            internal bool? GetCrop()
            {
                return crop;
            }

            public Builder SetCrop(bool crop)
            {
                this.crop = crop;
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

            public DownloadPodSpaceImageRequest Build()
            {
                var hasErrorFields = this.ValidateByAttribute();

                if (hasErrorFields.Any())
                {
                    throw PodException.BuildException(new InvalidParameter(hasErrorFields));
                }

                return new DownloadPodSpaceImageRequest(this);
            }
        }
    }
}
