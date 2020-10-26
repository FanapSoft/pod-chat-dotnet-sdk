using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Apache.NMS;
using POD_Async.Exception;
using POD_Async.Model;

namespace POD_Async.Base
{
    public class AsyncConfig
    {
        public static Builder ConcreteBuilder => new Builder();
        public string QueueUrl { get; }
        public string QueueNm { get; }
        public string Protocol { get; }
        public string QueueUsername { get; }
        public string QueuePassword { get; }
        public string QueueReceive { get; }
        public string QueueSend { get; }
        public long? QueueConnectionTimeout { get; }
        public string ServerName { get; }
        public string FileServer { get; }
        public string PlatformHost { get; }
        public string SsoHost { get; }
        public int ConsumersCount { get; } = 1;
        public AcknowledgementMode AckMode { get; }
        public AcknowledgementMode ConnectionAckMode { get; }

        public AsyncConfig(Builder builder)
        {
            QueueUrl = builder.GetQueueUrl();
            QueueUsername = builder.GetQueueUsername();
            QueuePassword = builder.GetQueuePassword();
            QueueReceive = builder.GetQueueReceive();
            QueueSend = builder.GetQueueSend();
            QueueConnectionTimeout = builder.GetQueueConnectionTimeout();
            ServerName = builder.GetServerName();
            FileServer = builder.GetFileServer();
            PlatformHost = builder.GetPlatformHost();
            SsoHost = builder.GetSsoHost();
            ConsumersCount = builder.GetConsumersCount();
            AckMode = builder.GetAckMode();
            ConnectionAckMode = builder.GetConnectionAckMode();
        }
        public class Builder
        {
            private string token;
            private long chatId;
            private string queueNm;
            private string protocol;
            [Required] private string queueUrl;
            [Required] private string queueUsername;
            [Required] private string queuePassword;
            [Required] private string queueReceive;
            [Required] private string queueSend;
            [Required] private long? queueConnectionTimeout;
            [Required] private string serverName;
            [Url] [Required] private string fileServer;
            [Url] [Required] private string platformHost;
            [Url] [Required] private string ssoHost;
            private int consumersCount;
            private AcknowledgementMode ackMode;
            private AcknowledgementMode connectionAckMode;

            public string GetQueueUrl()
            {
                return queueUrl;
            }
            public Builder SetQueueUrl(List<QueueUrl> queueUrl)
            {
                var sb = new StringBuilder("failover:(");
                sb.Append(string.Join(",", queueUrl.Select(_ => _.Url)));
                sb.Append(")");
                this.queueUrl = sb.ToString();
                return this;
            }

            public string GetQueueUsername()
            {
                return queueUsername;
            }
            public Builder SetQueueUsername(string queueUsername)
            {
                this.queueUsername = queueUsername;
                return this;
            }

            public string GetQueuePassword()
            {
                return queuePassword;
            }
            public Builder SetQueuePassword(string queuePassword)
            {
                this.queuePassword = queuePassword;
                return this;
            }

            public string GetQueueReceive()
            {
                return queueReceive;
            }
            public Builder SetQueueReceive(string queueReceive)
            {
                this.queueReceive = queueReceive;
                return this;
            }

            public string GetQueueSend()
            {
                return queueSend;
            }
            public Builder SetQueueSend(string queueSend)
            {
                this.queueSend = queueSend;
                return this;
            }

            public long? GetQueueConnectionTimeout()
            {
                return queueConnectionTimeout;
            }
            public Builder SetQueueConnectionTimeout(long queueConnectionTimeout)
            {
                this.queueConnectionTimeout = queueConnectionTimeout;
                return this;
            }

            public string GetServerName()
            {
                return serverName;
            }
            public Builder SetServerName(string serverName)
            {
                this.serverName = serverName;
                return this;
            }

            public string GetFileServer()
            {
                return fileServer;
            }
            public Builder SetFileServer(string fileServer)
            {
                this.fileServer = fileServer;
                return this;
            }

            public string GetPlatformHost()
            {
                return platformHost;
            }
            public Builder SetPlatformHost(string platformHost)
            {
                this.platformHost = platformHost;
                return this;
            }

            public string GetSsoHost()
            {
                return ssoHost;
            }
            public Builder SetSsoHost(string ssoHost)
            {
                this.ssoHost = ssoHost;
                return this;
            }

            public int GetConsumersCount()
            {
                return consumersCount;
            }

            public Builder SetConsumersCount(int consumersCount)
            {
                this.consumersCount = consumersCount;
                return this;
            }

            public AcknowledgementMode GetAckMode()
            {
                return ackMode;
            }

            public Builder SetAckMode(AcknowledgementMode ackMode)
            {
                this.ackMode = ackMode;
                return this;
            }

            public AcknowledgementMode GetConnectionAckMode()
            {
                return connectionAckMode;
            }

            public Builder SetConnectionAckMode(AcknowledgementMode connectionAckMode)
            {
                this.connectionAckMode = connectionAckMode;
                return this;
            }

            public AsyncConfig Build()
            {
                var hasErrorFields = this.ValidateByAttribute();

                if (hasErrorFields.Any())
                {
                    throw PodException.BuildException(new InvalidParameter(hasErrorFields));
                }

                return new AsyncConfig(this);
            }
        }
    }
}
