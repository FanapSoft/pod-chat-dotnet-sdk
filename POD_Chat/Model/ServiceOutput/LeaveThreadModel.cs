
namespace POD_Chat.Model.ServiceOutput
{
    public class LeaveThreadModel
    {
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Image { get; set; }
        public int Id { get; set; }
        public long NotSeenDuration { get; set; }
        public long ThreadId { get; set; }
    }
}
