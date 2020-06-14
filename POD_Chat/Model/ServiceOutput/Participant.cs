using System.Collections.Generic;

namespace POD_Chat.Model.ServiceOutput
{
    public class Participant
    {
        public long Id { get; set; }
        public long CoreUserId { get; set; }
        public bool SendEnable { get; set; }
        public bool ReceiveEnable { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string CellphoneNumber { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public bool MyFriend { get; set; }
        public bool Online { get; set; }
        public long NotSeenDuration { get; set; }
        public long ContactId { get; set; }
        public string ContactName { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }
        public bool Blocked { get; set; }
        public bool Admin { get; set; }
        public bool Auditor { get; set; }
        public string KeyId { get; set; }
        public string[] Roles { get; set; }
        public string Username { get; set; }
        public Profile ChatProfileVO { get; set; }
    }
}
