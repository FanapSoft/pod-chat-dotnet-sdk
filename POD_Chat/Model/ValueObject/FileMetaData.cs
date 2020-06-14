using Newtonsoft.Json;

namespace POD_Chat.Model.ValueObject
{
    internal class FileMetaData
    {
        [JsonProperty("file")]
        internal FileMetaDataContent File { get; set; }

        [JsonProperty("name")]
        internal string Name { get; set; }

        [JsonProperty("id")]
        internal long Id { get; set; }

        [JsonProperty("fileHash")]
        internal string FileHash { get; set; }
    }
}
