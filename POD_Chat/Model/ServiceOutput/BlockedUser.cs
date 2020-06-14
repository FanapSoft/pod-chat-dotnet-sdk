
namespace POD_Chat.Model.ServiceOutput
{
    public class BlockedUser
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public string ProfileImage { get; set; }
        public long CoreUserId { get; set; }
        public Contact ContactVO { get; set; }
    }
}
