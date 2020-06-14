
namespace POD_Chat.Model.ServiceOutput
{
    public class LinkedUser
    {
        public long Id { get; set; }
        public long CoreUserId { get; set; }
        public string Username { get; set; }
        public string Nickname { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string NationalCode { get; set; }
    }
}
