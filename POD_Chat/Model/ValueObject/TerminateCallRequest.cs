﻿using POD_Async.Base;
using POD_Async.Exception;
using System.Linq;

namespace POD_Chat.Model.ValueObject
{
    public class TerminateCallRequest:ChatMessageContent
    {
        public static Builder ConcreteBuilder => new Builder();

        public long CallId { get; }

        public TerminateCallRequest(Builder builder)
        {
            CallId = builder.GetCallId();
            TypeCode = builder.GetTypeCode();
            UniqueId = builder.GetUniqueId();
        }

        public override string GetJsonContent()
        {
            return null;
        }

        public class Builder
        {
            private long callId;
            private string uniqueId;
            private string typeCode;


            internal long GetCallId()
            {
                return callId;
            }

            public Builder SetCallId(long callId)
            {
                this.callId = callId;
                return this;
            }

            internal string GetUniqueId()
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

            public TerminateCallRequest Build()
            {
                var hasErrorFields = this.ValidateByAttribute();

                if (hasErrorFields.Any())
                {
                    throw PodException.BuildException(new InvalidParameter(hasErrorFields));
                }

                return new TerminateCallRequest(this);
            }
        }

        
    }
}
