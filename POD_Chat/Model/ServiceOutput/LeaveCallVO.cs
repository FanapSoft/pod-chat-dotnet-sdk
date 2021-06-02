using System.Collections.Generic;

namespace POD_Chat.Model.ServiceOutput
{
    public class LeaveCallVO
    {
        public long CallId { get; set; }

        public List<CallParticipantVO> Participants { get; set; }
    }
}
