using Newtonsoft.Json;

namespace POD_Chat.Model.ValueObject
{
    internal class FileMetaDataContent
    {
        [JsonProperty("id")]
        internal long Id { get; set; }

        [JsonProperty("originalName")]
        internal string OriginalName { get; set; }

        [JsonProperty("link")]
        internal string Link { get; set; }

        [JsonProperty("hashCode")]
        internal string HashCode { get; set; }

        [JsonProperty("name")]
        internal string Name { get; set; }

        [JsonProperty("size")]
        internal long Size { get; set; }

        [JsonProperty("mimeType")]
        internal string MimeType { get; set; }
    }
}
