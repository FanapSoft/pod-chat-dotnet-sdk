using Newtonsoft.Json;
using POD_Async.Base;

namespace POD_Async.Model
{
    public class MessageVo<T>
    {
        [JsonProperty(PropertyName = "peerName")]
        public string PeerName { get; set; }


        [JsonProperty(PropertyName = "content")]
        public string Content { get; set; }


        [JsonProperty(PropertyName = "receivers")]
        public long[] Receivers { get; set; }


        [JsonProperty(PropertyName = "messageId")]
        public long? MessageId { get; set; }

        [JsonProperty(PropertyName = "ttl")]
        public long TTL = 10 * 60 * 1000;

        public MessageVo(string peerName, T content, long? messageId)
        {
            PeerName = peerName;
            Content = content.ToJsonWithNotNullProperties();
            MessageId = messageId;
        }
        public MessageVo(T content, long messageId,long[] recievers)
        {
            Content = content.ToJsonWithNotNullProperties();
            MessageId = messageId;
            Receivers = recievers;
        }
    }
}
