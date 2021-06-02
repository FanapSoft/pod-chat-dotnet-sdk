namespace POD_Chat.Model.ServiceOutput
{
    public class DeliverCallRequestVO
    {
        public int userId { get; set; }
        public CallStatus callStatus { get; set; }
        public bool mute { get; set; }
        public bool video { get; set; }
    }
}
