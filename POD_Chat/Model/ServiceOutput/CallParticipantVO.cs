using Newtonsoft.Json;

namespace POD_Chat.Model.ServiceOutput
{
    public class CallParticipantVO
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("joinTime")]
        public long JoinTime { get; set; }

        [JsonProperty("leaveTime")]
        public long LeaveTime { get; set; }

        [JsonProperty("userId")]
        public long UserId { get; set; }

        [JsonProperty("sendTopic")]
        public string SendTopic { get; set; }

        [JsonProperty("receiveTopic")]
        public string ReceiveTopic { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }
        
        [JsonProperty("callStatus")]
        public int CallStatus { get; set; }

        [JsonProperty("participantVO")]
        public Participant ParticipantVO { get; set; }
    }
}
