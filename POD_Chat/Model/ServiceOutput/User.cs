
namespace POD_Chat.Model.ServiceOutput
{
    public class User
    {
        public long Id { get; set; }
        public bool SendEnable { get; set; }
        public bool ReceiveEnable { get; set; }
        public string Name { get; set; }
        public string CellphoneNumber { get; set; }
        public string Image { get; set; }
        public string UserName { get; set; }
        public string LastSeen { get; set; }
        public string Email { get; set; }
        public long CoreUserId { get; set; }
        public Profile ChatProfileVO { get; set; }
    }
}
