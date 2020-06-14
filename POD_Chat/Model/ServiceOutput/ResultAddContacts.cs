using System.Collections.Generic;

namespace POD_Chat.Model.ServiceOutput
{
    public class ResultAddContacts
    {
        public long ContentCount { get; set; }
        public List<Contact> Contacts { get; set; }
    }
}

