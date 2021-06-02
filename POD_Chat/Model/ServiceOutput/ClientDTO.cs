namespace POD_Chat.Model.ServiceOutput
{
    public class ClientDTO
    {
        public string clientId { get; set; }
        public string topicReceive { get; set; }
        public string topicSend { get; set; }
        public string brokerAddress { get; set; }
        public string desc { get; set; }
        public string sendKey { get; set; }
    }
}
