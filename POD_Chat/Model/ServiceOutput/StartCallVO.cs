namespace POD_Chat.Model.ServiceOutput
{
    public class StartCallVO
    {
        public string cert_file { get; set; }
        public ClientDTO clientDTO { get; set; }
        public string callName { get; set; }
        public string callImage { get; set; }
    }
}
