using System;
using System.Collections.Generic;

namespace POD_Async.Exception
{
    public struct InvalidParameter : IExceptionType
    {
        public int Code => PodExceptionConsts.InvalidParameterCode;
        public string Message { get; }
        public string UniqueId => null;
        public InvalidParameter(IEnumerable<KeyValuePair<string, string>> parameters)
        {
            Message = $"{PodExceptionConsts.InvalidParameterMessage} :{Environment.NewLine} " + " {" + string.Join($" {Environment.NewLine} ", parameters) + " }";
        }
        public InvalidParameter(KeyValuePair<string, string> parameters)
        {
            Message = $"{PodExceptionConsts.InvalidParameterMessage} :{Environment.NewLine} " + " {" + string.Join($" {Environment.NewLine} ", parameters) + " }";
        }
    }
    public struct UnexpectedException : IExceptionType
    {
        public int Code => PodExceptionConsts.UnexpectedErrorCode;
        public string Message => PodExceptionConsts.UnexpectedErrorMessage;
        public string UniqueId => null;
    }

    public struct ConnectionErrorException : IExceptionType
    {
        public int Code => PodExceptionConsts.ConnectionErrorCode;
        public string Message => PodExceptionConsts.ConnectionErrorMessage;
        public string UniqueId => null;
    }

    public struct CoreException : IExceptionType
    {
        public int Code { get; }
        public string Message { get; }
        public string UniqueId { get; }
        public CoreException(int code, string message)
        {
            Code = code;
            Message = message;
            UniqueId = null;
        }
        public CoreException(int code, string message, string uniqueId)
        {
            Code = code;
            Message = message;
            UniqueId = uniqueId;
        }
    }

    public struct AsyncException : IExceptionType
    {
        public int Code { get; }
        public string Message { get; }
        public string UniqueId { get; }

        public AsyncException(int code, string message, string uniqueId)
        {
            Code = code;
            Message = message;
            UniqueId = uniqueId;
        }
    }
}
