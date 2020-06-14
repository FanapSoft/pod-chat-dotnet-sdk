
namespace POD_Async.Exception
{
    public class PodException : System.Exception
    {
        public int Code { get; }
        public string UniqueId { get; }
       
        private PodException(IExceptionType exception) : base(exception.Message)
        {
            Code = exception.Code;
            UniqueId = exception.UniqueId;
        }

        public static PodException BuildException(IExceptionType exception)
        {
            return new PodException(exception);
        }
    }


}

