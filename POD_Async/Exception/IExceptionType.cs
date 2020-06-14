using POD_Async.Core.ResultModel;

namespace POD_Async.Exception
{
    public interface IExceptionType
    {
        int Code { get; }
        string Message { get; }
        string UniqueId { get; }
    }
}
