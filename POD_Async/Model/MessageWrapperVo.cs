using Newtonsoft.Json;
using POD_Async.Base;

namespace POD_Async.Model
{
    public class MessageWrapperVo<T>
    {

        [JsonProperty(PropertyName = "type")]
        public int Type { get; set; }

        [JsonProperty(PropertyName = "content")]
        public string Content { get; set; }

        [JsonProperty(PropertyName = "trackerId")]
        public string TrackerId { get; set; }

        public MessageWrapperVo(AsyncMessageType type, T content)
        {
            Type = (int) type;
            Content = content.ToJsonWithNotNullProperties();
        }
    }
}
