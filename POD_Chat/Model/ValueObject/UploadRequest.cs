using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;
using System.Linq;
using POD_Async.Base;
using POD_Async.Exception;

namespace POD_Chat.Model.ValueObject
{
    public class UploadRequest
    {
        public static Builder ConcreteBuilder => new Builder();
        public string FilePath { get; }
        public string FileName { get; }
        public int XC { get; }
        public int YC { get; }
        public int HC { get; }
        public int WC { get; }
        public string MimeType { get; }
        public bool IsImage { get; }
        public FileInfo FileData { get; }
        public string UserGroupHash { get; internal set; }

        public UploadRequest(Builder builder)
        {
            FilePath = builder.GetFilePath();
            FileName = builder.GetFileName();
            MimeType = builder.GetMimeType();
            IsImage = MimeType.IsImage();
            FileData = builder.GetFileData();
            XC = builder.GetXC();
            YC = builder.GetYC();
            HC = builder.GetHC();
            WC = builder.GetWC();

            if (IsImage)
            {
                var img = new Bitmap(FilePath);

                if (HC <= 0)
                    HC = img.Height;

                if (WC <= 0)
                    WC = img.Width;
            }

            UserGroupHash = builder.GetUserGroupHash();
        }

        public class Builder
        {
            [Required]
            private string filePath;

            [Required]
            private string fileName;
            private int xC;
            private int yC;
            private int hC;
            private int wC;
            private string userGroupHash;

            internal string GetFilePath()
            {
                return filePath;
            }

            public Builder SetFilePath(string filePath)
            {
                this.filePath = filePath;
                return this;
            }

            internal string GetMimeType()
            {
                var extension = Path.GetExtension(filePath);
                var mimeType = MimeTypeMap.GetMimeType(extension);
                return mimeType;
            }

            internal FileInfo GetFileData()
            {
                return new FileInfo(filePath);
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

            internal int GetXC()
            {
                return xC;
            }

            public Builder SetXC(int xC)
            {
                this.xC = xC;
                return this;
            }

            internal int GetYC()
            {
                return yC;
            }

            public Builder SetYC(int yC)
            {
                this.yC = yC;
                return this;
            }

            internal int GetHC()
            {
                return hC;
            }

            public Builder SetHC(int hC)
            {
                this.hC = hC;
                return this;
            }

            internal int GetWC()
            {
                return wC;
            }

            public Builder SetWC(int wC)
            {
                this.wC = wC;
                return this;
            }

            internal string GetUserGroupHash()
            {
                return userGroupHash;
            }

            internal Builder SetUserGroupHash(string userGroupHash)
            {
                this.userGroupHash = userGroupHash;
                return this;
            }

            public UploadRequest Build()
            {
                var hasErrorFields = this.ValidateByAttribute();

                if (!File.Exists(filePath))
                {
                    hasErrorFields.Add(new System.Collections.Generic.KeyValuePair<string, string>(nameof(filePath), "File does not exist"));
                }

                if (hasErrorFields.Any())
                {
                    throw PodException.BuildException(new InvalidParameter(hasErrorFields));
                }

                return new UploadRequest(this);
            }
        }
    }
}
