using System;

namespace POD_Chat.Model.ServiceOutput
{
    public class Contact
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string CellphoneNumber { get; set; }
        public string UniqueId { get; set; }
        public LinkedUser LinkedUser { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? LastModifyDate { get; set; }
        public string ContactType { get; set; }
        public string UserName { get; set; }
    }
}
