using System.Collections.Generic;

namespace POD_Chat.Model.ServiceOutput
{
    public class GetThreadsModel
    {
        public List<Conversation> Threads { get; set; }
        public long? ContentCount { get; set; }
        public bool HasNext { get; set; }
        public long NextOffset { get; set; }
    }
}
