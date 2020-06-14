using POD_Async.Core.ResultModel;
using System.Collections.Generic;

namespace POD_Async.Core.ResultModel
{
    public class ResultSrv<T> : IResultSrv
    {
        public bool HasError { get; set; }
        public long MessageId { get; set; }
        public string ReferenceNumber { get; set; }
        public int ErrorCode { get; set; }
        public string Message { get; set; }
        public long Count { get; set; }
        public string Ott { get; set; }
        public T Result { get; set; }
        public KeyValuePair<string, object> Aggregations { get; set; }
        public string LastUpdated { get; set; }

        public ResultSrv()
        {

        }

        public ResultSrv(T result)
        {
            Result = result;
        }

        public ResultSrv(T result, long count)
        {
            Count = count;
            Result = result;
        }

        public ResultSrv(T result, long count, KeyValuePair<string, object> aggregations)
        {
            Result = result;
            Count = count;
            Aggregations = aggregations;
        }

        public ResultSrv(T result, int errorCode, string message)
        {
            Result = result;
            Message = message;
            ErrorCode = errorCode;
            if (message == null || message.Trim().Equals(""))
            {
                Message = errorCode.ToString();
            }
        }
    }
}
