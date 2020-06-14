using System.Collections.Generic;

namespace POD_Chat.Model.ServiceOutput
{
    public class GetHistoryModel
    {
        public List<MessageVO> History { get; set; }
        public long? ContentCount { get; set; }
        public bool HasNext { get; set; }
        public long NextOffset { get; set; }
    }
}
