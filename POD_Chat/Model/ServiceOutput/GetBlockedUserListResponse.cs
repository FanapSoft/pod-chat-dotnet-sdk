using System.Collections.Generic;

namespace POD_Chat.Model.ServiceOutput
{
    public class GetBlockedUserListResponse
    {
        public List<BlockedUser> Contacts { get; set; }
        public int? ContentCount { get; set; }
        public bool HasNext { get; set; }
        public long NextOffset { get; set; }
    }
}
