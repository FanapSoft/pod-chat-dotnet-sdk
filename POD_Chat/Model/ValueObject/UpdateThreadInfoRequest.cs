using POD_Async.Base;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Newtonsoft.Json;
using POD_Async.Exception;
using System.Collections.Generic;

namespace POD_Chat.Model.ValueObject
{
    public class UpdateThreadInfoRequest : ChatMessageContent
    {
        public static Builder ConcreteBuilder => new Builder();

        [JsonProperty("title")]
        public string Title { get; }

        [JsonProperty("description")]
        public string Description { get; }

        [JsonProperty("image")]
        public string Image { get; }

        [JsonProperty("metadata")]
        public string Metadata { get; internal set; }

        [JsonIgnore]
        public long? ThreadId { get; }

        [JsonIgnore]
        public UploadRequest UploadInput { get; }

        public UpdateThreadInfoRequest(Builder builder)
        {
            Title = builder.GetTitle();
            Description = builder.GetDescription();
            Image = builder.GetImage();
            Metadata = builder.GetMetadata();
            ThreadId = builder.GetThreadId();
            UploadInput = builder.GetUploadInput();

            if (UploadInput != null)
            {
                UploadInput.UserGroupHash = builder.GetUserGroupHash();
            }

            TypeCode = builder.GetTypeCode();
            UniqueId = builder.GetTypeCode();
        }

        public override string GetJsonContent()
        {
            return this.ToJsonWithNotNullProperties();
        }

        public class Builder
        {
            private string title;
            private string description;
            private string image;
            private string metadata;

            [Required]
            private long? threadId;
            private UploadRequest uploadInput;
            private string userGroupHash;
            private string typeCode;
            private string uniqueId;

            internal string GetTitle()
            {
                return title;
            }

            public Builder SetTitle(string title)
            {
                this.title = title;
                return this;
            }

            internal string GetDescription()
            {
                return description;
            }

            public Builder SetDescription(string description)
            {
                this.description = description;
                return this;
            }

            internal string GetImage()
            {
                return image;
            }

            public Builder SetImage(string image)
            {
                this.image = image;
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

            internal long? GetThreadId()
            {
                return threadId;
            }

            public Builder SetThreadId(long threadId)
            {
                this.threadId = threadId;
                return this;
            }

            public UploadRequest GetUploadInput()
            {
                return uploadInput;
            }

            /// <summary>
            /// Type of image for thread profile must be jpeg|bmp|jpg|png
            /// </summary>
            public Builder SetUploadInput(UploadRequest uploadInput)
            {
                this.uploadInput = uploadInput;
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

            public string GetUniqueId()
            {
                return uniqueId;
            }

            public Builder SetUniqueId(string uniqueId)
            {
                this.uniqueId = uniqueId;
                return this;
            }

            public string GetTypeCode()
            {
                return typeCode;
            }

            public Builder SetTypeCode(string typeCode)
            {
                this.typeCode = typeCode;
                return this;
            }

            public UpdateThreadInfoRequest Build()
            {
                var hasErrorFields = this.ValidateByAttribute();
                if (uploadInput != null)
                {
                    if (!uploadInput.IsImage)
                        hasErrorFields.Add(new KeyValuePair<string, string>("ThreadProfile",
                            "Type of file must be jpeg|bmp|jpg|png"));

                    if (string.IsNullOrEmpty(userGroupHash))
                        hasErrorFields.Add(new KeyValuePair<string, string>(nameof(userGroupHash),
                            "Fill UserGroupHash field for changing thread profile"));
                }

                if (hasErrorFields.Any())
                {
                    throw PodException.BuildException(new InvalidParameter(hasErrorFields));
                }

                return new UpdateThreadInfoRequest(this);
            }
        }
    }
}
