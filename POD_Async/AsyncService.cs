using System.Collections.Generic;
using System.Text;
using Apache.NMS;
using Apache.NMS.ActiveMQ;
using POD_Async.Model;
using Newtonsoft.Json;
using ActiveMqMessage = Apache.NMS.ActiveMQ.Commands.Message;
using POD_Async.Base;
using POD_Async.Exception;

namespace POD_Async
{
    public delegate void AsyncMessageReceived(string message);

    public class AsyncService
    {
        #region Event

        public event AsyncMessageReceived AsyncMessageReceived;
        public event AsyncMessageReceived AsyncError;

        #endregion Event

        #region Field

        private readonly AsyncConfig config;

        #region Producer

        private IConnection producerConnection;
        private IMessageProducer producer;
        private ISession producerSession;

        #endregion Producer

        #region Consumer

        private IConnection consumerConnection;
        private IMessageConsumer consumer;
        private ISession consumerSession;

        #endregion Consumer

        #endregion Field

        #region Constructor

        public AsyncService(AsyncConfig config)
        {
            this.config = config ?? throw PodException.BuildException(
                              new InvalidParameter(new KeyValuePair<string, string>(nameof(config), "This field is Required")));
            CreateProducer();
            CreateConsumer();
        }

        #endregion Constructor

        #region Create_Connection

        private void CreateProducer()
        {
            producerConnection = CreateConnection();
            producerSession = producerConnection.CreateSession(AcknowledgementMode.ClientAcknowledge);
            producer = producerSession.CreateProducer(producerSession.GetQueue(config.QueueSend));
        }

        private void CreateConsumer()
        {
            consumerConnection = CreateConnection();
            consumerSession = consumerConnection.CreateSession(AcknowledgementMode.ClientAcknowledge);
            consumer = consumerSession.CreateConsumer(consumerSession.GetQueue(config.QueueReceive));
            consumer.Listener += OnMessageReceived;
        }

        private IConnection CreateConnection()
        {
            IConnectionFactory factory = new ConnectionFactory(config.QueueUrl);
            var connection = factory.CreateConnection(config.QueueUsername, config.QueuePassword);
            connection.AcknowledgementMode = AcknowledgementMode.ClientAcknowledge;
            connection.Start();
            return connection;
        }

        #endregion Create_Connection

        #region Response_Handling

        private void OnMessageReceived(IMessage message)
        {
            try
            {
                var messageByte = ((ActiveMqMessage)message)?.Content;
                if (messageByte != null)
                {
                    var response = Encoding.UTF8.GetString(messageByte);
                    var clientMessage = JsonConvert.DeserializeObject<ClientMessage>(response);

                    switch (clientMessage.type)
                    {
                        case AsyncMessageType.ERROR_MESSAGE:
                            HandleOnErrorMessage(clientMessage);
                            break;

                        case AsyncMessageType.MESSAGE_ACK_NEEDED:
                            HandleOnMessageAckNeeded(clientMessage);
                            break;

                        case AsyncMessageType.MESSAGE_SENDER_ACK_NEEDED:
                            HandleOnMessageAckNeeded(clientMessage);
                            break;

                        case AsyncMessageType.MESSAGE:
                            HandleOnMessage(clientMessage);
                            break;
                    }
                }
                else
                {               
                    throw PodException.BuildException(new AsyncException(0, "ActiveMqMessage does not have any content.", ""));
                }
            }
            catch (System.Exception ex)
            {
                PodLogger.Logger.Error($"{ex.Message}\r\nException Source :\r\n{ex.Source}");
                throw ex;
            }
        }

        private void HandleOnMessage(ClientMessage clientMessage)
        {
            PodLogger.Logger.Info(clientMessage.content);
            AsyncMessageReceived?.Invoke(clientMessage.content);
        }

        private void HandleOnMessageAckNeeded(ClientMessage clientMessage)
        {
            PodLogger.Logger.Info(clientMessage.content);
            AsyncMessageReceived?.Invoke(clientMessage.content);
            SendAckMessage(clientMessage);
        }

        private void HandleOnErrorMessage(ClientMessage clientMessage)
        {
            var asyncException = new AsyncErrorMessage
            {
                ErrorCode = (int)AsyncMessageType.ERROR_MESSAGE,
                ErrorMessage = clientMessage.content
            };

            var errorJson = asyncException.ToJson();
            PodLogger.Logger.Error(errorJson);
            AsyncError?.Invoke(errorJson);           
        }

        #endregion Response_Handling

        #region Send & Wrap Message

        public void SendMessage<T>(T content, AsyncMessageType asyncMessageType = AsyncMessageType.MESSAGE)
        {
            var wrappedMessage = WrapChatMessage(content, asyncMessageType);
            PodLogger.Logger.Info(wrappedMessage);
            var bytes = Encoding.UTF8.GetBytes(wrappedMessage);
            var bytesMessage = producer.CreateBytesMessage(bytes);
            //bytesMessage.NMSDeliveryMode = MsgDeliveryMode.Persistent;
            producer.Send(bytesMessage);
        }

        private void SendAckMessage(ClientMessage clientMessage)
        {
            var messageSenderAckNeeded = new Message
            {
                messageId = clientMessage.id
            };

            var wrappedMessage = AsyncWrap(messageSenderAckNeeded, AsyncMessageType.ACK);
            PodLogger.Logger.Info(wrappedMessage);
            var bytes = Encoding.UTF8.GetBytes(wrappedMessage);
            var bytesMessage = producer.CreateBytesMessage(bytes);
            producer.Send(bytesMessage);
        }

        private string WrapChatMessage<T>(T content, AsyncMessageType type)
        {
            var messageVo = new MessageVo<T>(config.ServerName, content, null);
            var text = AsyncWrap(messageVo, type);
            return text;
        }

        private string AsyncWrap<T>(T content, AsyncMessageType type)
        {
            var messageWrapperVo = new MessageWrapperVo<T>(type, content);
            var text = JsonConvert.SerializeObject(messageWrapperVo);
            return text;
        }

        #endregion Send & Wrap Message

        #region ShutDown

        public void Dispose()
        {
            producerSession.Dispose();
            consumerSession.Dispose();
            consumerConnection.Dispose();
            producerConnection.Dispose();
            producer.Dispose();
            consumer.Dispose();
        }

        #endregion
    }
}
