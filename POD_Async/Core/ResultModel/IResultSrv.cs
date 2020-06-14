namespace POD_Async.Core.ResultModel
{
    public interface IResultSrv
    {
        bool HasError { get; set; }
        long MessageId { get; set; }
        string ReferenceNumber { get; set; }
        int ErrorCode { get; set; }
        string Message { get; set; }
        long Count { get; set; }
        string Ott { get; set; }
    }
}
