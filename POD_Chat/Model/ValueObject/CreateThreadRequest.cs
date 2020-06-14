using POD_Async.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Newtonsoft.Json;
using POD_Chat.Base.Enum;
using POD_Async.Exception;

namespace POD_Chat.Model.ValueObject
{
    public class CreateThreadRequest : ChatMessageContent
    {
        public static Builder ConcreteBuilder => new Builder();

        [JsonProperty("type")]
        public ThreadType? Type { get; }

        [JsonProperty("invitees")]
        public List<InviteVo> Invitees { get; }

        [JsonProperty("title")]
        public string Title { get; }

        [JsonProperty("description")]
        public string Description { get; }

        [JsonProperty("image")]
        public string Image { get; }

        [JsonProperty("metadata")]
        public string Metadata { get; }

        [JsonProperty("uniqueName")]
        public string UniqueName { get; }

        [JsonIgnore]
        public UploadRequest UploadInput { get; }

        public CreateThreadRequest(Builder builder)
        {
            Type = builder.GetType();
            Invitees = builder.GetInvitees();
            Title = builder.GetTitle();
            Description = builder.GetDescription();
            Image = builder.GetImage();
            Metadata = builder.GetMetadata();
            UniqueName = builder.GetUniqueName();
            UploadInput = builder.GetUploadInput();
            TypeCode = builder.GetTypeCode();
        }

        public override string GetJsonContent()
        {
            return this.ToJsonWithNotNullProperties();
        }

        public class Builder
        {
            [Required]
            private ThreadType? type;

            [Required]
            private List<InviteVo> invitees;
            private string title;
            private string description;
            private string image;
            private string metadata;
            private string uniqueName;
            private UploadRequest uploadInput;
            private string typeCode;

            internal List<InviteVo> GetInvitees()
            {
                return invitees;
            }

            public Builder SetInvitees(List<InviteVo> invitees)
            {
                this.invitees = invitees;
                return this;
            }

            internal string GetTitle()
            {
                return title;
            }

            public Builder SetTitle(string title)
            {
                this.title = title;
                return this;
            }

            internal new ThreadType? GetType()
            {
                return type;
            }

            public Builder SetType(ThreadType type)
            {
                this.type = type;
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

            internal string GetUniqueName()
            {
                return uniqueName;
            }

            public Builder SetUniqueName(string uniqueName)
            {
                this.uniqueName = uniqueName;
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

            internal string GetTypeCode()
            {
                return typeCode;
            }

            public Builder SetTypeCode(string typeCode)
            {
                this.typeCode = typeCode;
                return this;
            }

            public CreateThreadRequest Build()
            {
                var hasErrorFields = this.ValidateByAttribute();
                var threadType = (ThreadType)type;
                if (threadType == ThreadType.PUBLIC_GROUP || threadType == ThreadType.CHANNEL_GROUP)
                    if (string.IsNullOrEmpty(uniqueName))
                        hasErrorFields.Add(new KeyValuePair<string, string>("uniqueName", "The Builder field is Required for PUBLIC_GROUP or CHANNEL_GROUP"));

                if (uploadInput!=null && !uploadInput.IsImage)
                {
                    hasErrorFields.Add(new KeyValuePair<string, string>("ThreadProfile", "Type of File must be jpeg|bmp|jpg|png"));
                }

                if (hasErrorFields.Any())
                {
                    throw PodException.BuildException(new InvalidParameter(hasErrorFields));
                }

                return new CreateThreadRequest(this);
            }
        }
    }
}
