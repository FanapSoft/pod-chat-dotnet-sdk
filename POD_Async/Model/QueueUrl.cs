using System.ComponentModel.DataAnnotations;
using System.Linq;
using POD_Async.Base;
using POD_Async.CustomAttribute;
using POD_Async.Exception;

namespace POD_Async.Model
{
    public class QueueUrl
    {
        public static Builder ConcreteBuilder => new Builder();
        public string Url { get; }

        public QueueUrl(Builder builder)
        {
            Url = "tcp://" + builder.GetIp() + ":" + builder.GetPort();
        }

        public class Builder
        {
            [IPAddress]
            [Required]
            private string ip;

            [Required]
            private int? port;

            public string GetIp()
            {
                return ip;
            }
            public Builder SetIp(string ip)
            {
                this.ip = ip;
                return this;
            }
            public int? GetPort()
            {
                return port;
            }
            public Builder SetPort(int port)
            {
                this.port = port;
                return this;
            }

            public QueueUrl Build()
            {
                var hasErrorFields = this.ValidateByAttribute();

                if (hasErrorFields.Any())
                {
                    throw PodException.BuildException(new InvalidParameter(hasErrorFields));
                }

                return new QueueUrl(this);
            }
        }
    }
}
