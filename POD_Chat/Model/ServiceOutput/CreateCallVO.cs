using POD_Chat.Base.Enum;
using POD_Chat.Model.ValueObject;
using System.Collections.Generic;

namespace POD_Chat.Model.ServiceOutput
{
    public class CreateCallVO
    {
        public List<InviteVo> invitees { get; set; }
        public CallType type { get; set; }
        public long creatorId { get; set; }
        public Participant creatorVO { get; set; }
        public Conversation conversationVO { get; set; }
        public long threadId { get; set; }
        public long callId { get; set; }
        public bool group { get; set; }
    }
}
