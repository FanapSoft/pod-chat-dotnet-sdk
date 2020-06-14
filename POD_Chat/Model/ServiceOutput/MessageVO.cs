using Newtonsoft.Json;
using POD_Async.Base;
using POD_Chat.Model.ValueObject;

namespace POD_Chat.Model.ServiceOutput
{
    public class MessageVO : ChatMessageContent
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("uniqueId")]
        public string UniqueId { get; set; }

        [JsonProperty("previousId")]
        public long PreviousId { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("messageType")]
        public int MessageType { get; set; }

        [JsonProperty("edited")]
        public bool Edited { get; set; }

        [JsonProperty("editable")]
        public bool Editable { get; set; }

        [JsonProperty("deletable")]
        public bool Deletable { get; set; }

        [JsonProperty("participant")]
        public Participant Participant { get; set; }

        [JsonProperty("conversation")]
        public Conversation Conversation { get; set; }

        [JsonProperty("time")]
        public long Time { get; set; }

        [JsonProperty("timeNanos")]
        public long TimeNanos { get; set; }

        [JsonProperty("delivered")]
        public bool Delivered { get; set; }

        [JsonProperty("seen")]
        public bool Seen { get; set; }

        [JsonProperty("metadata")]
        public string Metadata { get; set; }

        [JsonProperty("systemMetadata")]
        public string SystemMetadata { get; set; }

        [JsonProperty("replyInfoVO")]
        public ReplyInfoVO ReplyInfoVo { get; set; }

        [JsonProperty("forwardInfo")]
        public ForwardInfo ForwardInfo { get; set; }

        [JsonProperty("mentioned")]
        public bool Mentioned { get; set; }

        [JsonProperty("pinned")]
        public bool Pinned { get; set; }

        public override string GetJsonContent()
        {
            return this.ToJsonWithNotNullProperties();
        }
    }
}
