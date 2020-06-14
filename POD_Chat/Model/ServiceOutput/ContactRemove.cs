
namespace POD_Chat.Model.ServiceOutput
{
    public class ContactRemove
    {
        public bool HasError { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public bool Result { get; set; }
    }
}
