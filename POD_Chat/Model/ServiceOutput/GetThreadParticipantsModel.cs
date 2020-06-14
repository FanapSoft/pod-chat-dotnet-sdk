using System.Collections.Generic;

namespace POD_Chat.Model.ServiceOutput
{
    public class GetThreadParticipantsModel
    {
        public List<Participant> Participants { get; set; }
        public bool HasNext { get; set; }
        public long NextOffset { get; set; }
        public int? ContentCount { get; set; }
    }
}
