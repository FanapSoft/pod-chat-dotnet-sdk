
using Newtonsoft.Json;

namespace POD_Chat.Model.ValueObject
{
    public abstract class ChatMessageContent
    {
        public abstract string GetJsonContent();

        [JsonProperty(PropertyName = "typeCode")]
        public string TypeCode { get; set; }

        [JsonProperty(PropertyName = "uniqueId")]
        public string UniqueId { get; set; }
    }
}
