using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace POD_Chat.Model.ServiceOutput
{
    public class CallVO
    {
        //CallId If exist
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("creatorId")]
        public long CreatorId { get; set; }

        [JsonProperty("type")]
        public int Type { get; set; }

        [JsonProperty("createTime")]
        public long? CreateTime { get; set; }

        [JsonProperty("startTime")]
        public long? StartTime { get; set; }

        [JsonProperty("endTime")]
        public long? EndTime { get; set; }

        [JsonProperty("status")]
        public CallStatus? Status { get; set; }

        [JsonProperty("group")]
        public bool IsGroup { get; set; }

        [JsonProperty("callParticipants")]
        public List<Participant> CallParticipants { get; set; }

        [JsonProperty("partnerParticipantVO")]
        public Participant PartnerParticipantVO { get; set; }
    }
}
