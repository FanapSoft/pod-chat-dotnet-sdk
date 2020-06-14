
namespace POD_Chat.Model.ServiceOutput
{
    public class ThingVO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public bool Active { get; set; }
        public bool ChatSendEnable { get; set; }
        public bool ChatReceiveEnable { get; set; }
        public Owner Owner { get; set; }
        public Owner Creator { get; set; }
        public bool Bot { get; set; }
    }
}
