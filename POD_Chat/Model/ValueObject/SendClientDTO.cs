using Newtonsoft.Json;
using POD_Async.Base;

namespace POD_Chat.Model.ValueObject
{
    public class SendClientDTO : ChatMessageContent
    {
        public enum CallSendClientDTOType { 
            WEB = 1,
            ANDROID = 2,
            DESKTOP = 3
        }

        public static Builder ConcreteBuilder => new Builder();


        [JsonProperty("clientId")]
        public string ClientId { get; }

        [JsonProperty("clientType")]
        public CallSendClientDTOType ClientType { get; }

        [JsonProperty("deviceId")]
        public string DeviceId { get; }

        [JsonProperty("mute")]
        public bool Mute { get; }

        [JsonProperty("video")]
        public bool Video { get; }

        [JsonProperty("desc")]
        public string Desc { get; }

        [JsonProperty("creatorClientDto")]
        public SendClientDTO CreatorClientDto { get; }

        public SendClientDTO(Builder builder)
        {
            ClientId = builder.GetClientId();
            ClientType = builder.GetClientType();
            DeviceId = builder.GetDeviceId();
            Mute = builder.GetIsMute();
            Video = builder.GetIsVideo();
            Desc = builder.GetDesc();
            TypeCode = builder.GetTypeCode();
            UniqueId = builder.GetUniqueId();
        }

        public override string GetJsonContent()
        {
            return this.ToJsonWithNotNullProperties();
        }


        public class Builder
        {

            private string clientId;
            private CallSendClientDTOType clientType = CallSendClientDTOType.DESKTOP;
            private string deviceId;
            private bool mute = false;
            private bool video = false;
            private string desc;
            private string typeCode;
            private string uniqueId;

            internal string GetClientId()
            {
                return clientId;
            }

            internal CallSendClientDTOType GetClientType()
            {
                return clientType;
            }

            internal string GetDeviceId()
            {
                return deviceId;
            }

            internal bool GetIsMute()
            {
                return mute;
            }

            internal bool GetIsVideo()
            {
                return video;
            }
            internal string GetDesc()
            {
                return desc;
            }

            public Builder SetClientId(string clientId) {
                this.clientId = clientId;
                return this;
            }

            public Builder SetClientType(CallSendClientDTOType clientType)
            {
                this.clientType = clientType;
                return this;
            }

            public Builder SetDeviceId(string deviceId)
            {
                this.deviceId = deviceId;
                return this;
            }

            public Builder SetMute(bool mute)
            {
                this.mute = mute;
                return this;
            }

            public Builder SetVideo(bool video)
            {
                this.video = video;
                return this;
            }

            public Builder SetDesc(string desc)
            {
                this.desc = desc;
                return this;
            }
            public string GetUniqueId()
            {
                return uniqueId;
            }

            public Builder SetUniqueId(string uniqueId)
            {
                this.uniqueId = uniqueId;
                return this;
            }

            public string GetTypeCode()
            {
                return typeCode;
            }

            public Builder SetTypeCode(string typeCode)
            {
                this.typeCode = typeCode;
                return this;
            }

            public SendClientDTO Build()
            {
                return new SendClientDTO(this);
            }
        }
    }
}
