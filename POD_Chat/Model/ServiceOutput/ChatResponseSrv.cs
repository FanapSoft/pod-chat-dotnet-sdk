
namespace POD_Chat.Model.ServiceOutput
{
    public class ChatResponseSrv<T>
    {
        public bool HasError { get; set; }
        public string ErrorMessage { get; set; }
        public long ErrorCode { get; set; }
        public string UniqueId { get; set; }
        public long? SubjectId { get; set; }
        public T Result { get; set; }
    }
}
