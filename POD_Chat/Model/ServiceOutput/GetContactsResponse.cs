using System.Collections.Generic;

namespace POD_Chat.Model.ServiceOutput
{
    public class GetContactsResponse
    {
        public List<Contact> Contacts { get; set; }
        public long? ContentCount { get; set; }
        public bool HasNext { get; set; }
        public long NextOffset { get; set; }
    }
}
