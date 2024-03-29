﻿using System.ComponentModel.DataAnnotations;
using System.Linq;
using Newtonsoft.Json;
using POD_Async.Base;
using POD_Async.Exception;

namespace POD_Chat.Model.ValueObject
{
    public class IsPublicThreadNameAvailableRequest : ChatMessageContent
    {
        public static Builder ConcreteBuilder => new Builder();

        [JsonProperty("uniqueName")]
        public string UniqueName { get; }

        public IsPublicThreadNameAvailableRequest(Builder builder)
        {
            UniqueName = builder.GetUniqueName();
            TypeCode = builder.GetTypeCode();
            UniqueId = builder.GetUniqueId();
        }

        public override string GetJsonContent()
        {
            return UniqueName;
        }

        public class Builder
        {
            [Required]
            private string uniqueName;
            private string typeCode;
            private string uniqueId;

            internal string GetUniqueName()
            {
                return uniqueName;
            }

            public Builder SetUniqueName(string uniqueName)
            {
                this.uniqueName = uniqueName;
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

            public IsPublicThreadNameAvailableRequest Build()
            {
                var hasErrorFields = this.ValidateByAttribute();

                if (hasErrorFields.Any())
                {
                    throw PodException.BuildException(new InvalidParameter(hasErrorFields));
                }

                return new IsPublicThreadNameAvailableRequest(this);
            }
        }
    }
}
