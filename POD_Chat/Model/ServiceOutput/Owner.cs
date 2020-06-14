
namespace POD_Chat.Model.ServiceOutput
{
    public class Owner
    {
        public long Id { get; set; }
        public long CoreUserId { get; set; }
        public int LastVersion { get; set; }
        public string NickName { get; set; }
        public bool Active { get; set; }
        public bool ChatSendEnable { get; set; }
        public bool ChatReceiveEnable { get; set; }
        public bool Guest { get; set; }
        public int IssuerCode { get; set; }
        public long SsoId { get; set; }
        public long ContactsLastSeenUpdate { get; set; }
        public string Profile { get; set; }
        public bool Bot { get; set; }
        public string FullName { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public Profile ChatProfileVo { get; set; }
    }
}
