using System.Collections.Generic;

namespace POD_Chat.Model.ServiceOutput
{
    public class Contacts
    {
        public bool HasError { get; set; }
        public string ReferenceNumber { get; set; }
        public string Message { get; set; }
        public int ErrorCode { get; set; }
        public int Count { get; set; }
        public string Ott { get; set; }
        public List<Contact> Result { get; set; }
    }
}
