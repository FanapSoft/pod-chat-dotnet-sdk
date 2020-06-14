using Newtonsoft.Json;

namespace POD_Chat.Model.ValueObject
{
    public class ChatMessageVo
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("typeCode")]
        public string TypeCode { get; set; }

        [JsonProperty("tokenIssuer")]
        public int? TokenIssuer { get; set; }

        [JsonProperty("type")]
        public int? Type { get; set; }

        [JsonProperty("messageType")]
        public int? MessageType { get; set; }

        [JsonProperty("subjectId")]
        public long? SubjectId { get; set; }

        [JsonProperty("uniqueId")]
        public string UniqueId { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("contentCount")]
        public int? ContentCount { get; set; }

        [JsonProperty("systemMetadata")]
        public string SystemMetadata { get; set; }

        [JsonProperty("metadata")]
        public string Metadata { get; set; }

        [JsonProperty("repliedTo")]
        public long? RepliedTo { get; set; }
    }
}
