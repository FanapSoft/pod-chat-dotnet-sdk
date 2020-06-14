using System.Collections.Generic;

namespace POD_Chat.Model.ServiceOutput
{
    public class Conversation
    {
        public long? Id { get; set; }
        public long? JoinDate { get; set; }
        public Participant Inviter { get; set; }
        public string Title { get; set; }
        public List<Participant> Participants { get; set; }
        public long? Time { get; set; }
        public string LastMessage { get; set; }
        public string LastParticipantName { get; set; }
        public bool? Group { get; set; }
        public long? Partner { get; set; }
        public string LastParticipantImage { get; set; }
        public long? UnreadCount { get; set; }
        public long? LastSeenMessageId { get; set; }
        public long? LastSeenMessageTime { get; set; }
        public int? LastSeenMessageNanos { get; set; }
        public MessageVO LastMessageVO{ get; set; }
        public long? PartnerLastSeenMessageId { get; set; }
        public long? PartnerLastSeenMessageTime { get; set; }
        public int? PartnerLastSeenMessageNanos { get; set; }
        public long? PartnerLastDeliveredMessageId { get; set; }
        public long? PartnerLastDeliveredMessageTime { get; set; }
        public int? PartnerLastDeliveredMessageNanos { get; set; }
        public int? Type { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string Metadata { get; set; }
        public bool? Mute { get; set; }
        public long? ParticipantCount { get; set; }
        public bool? CanEditInfo { get; set; }
        public bool? CanSpam { get; set; }
        public bool? Admin { get; set; }
        public bool? Mentioned { get; set; }
        public bool? Pin { get; set; }
        public string UniqueName { get; set; }
        public string PinMessage { get; set; }
        public string UserGroupHash { get; set; }
    }
}
