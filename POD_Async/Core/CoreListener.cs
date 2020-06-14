using POD_Async.Base;
using POD_Async.Core.ResultModel;
using POD_Async.Exception;
using RestSharp;

namespace POD_Async.Core
{
    public static class CoreListener
    {
        public static void GetResult<T>(IRestResponse<ResultSrv<T>> restResponse, out ResultSrv<T> output)
        {
            PodLogger.Logger.Info(((RestResponseBase)restResponse).ResponseUri.OriginalString);
            output = new ResultSrv<T>();
            if (restResponse.IsSuccessful)
            {
                var resultSrv = restResponse.Data;
                if (resultSrv.HasError)
                {
                    PodLogger.Logger.Error(restResponse.Content);
                    throw PodException.BuildException(new CoreException(resultSrv.ErrorCode, resultSrv.Message));
                }

                PodLogger.Logger.Info(restResponse.Content);
                output = resultSrv;
            }
            else
            {
                OnFailure(restResponse);
            }
        }

        private static void OnFailure(IRestResponse restResponse)
        {
            PodLogger.Logger.Error(restResponse.Content);
            if (restResponse.ResponseStatus == ResponseStatus.TimedOut || restResponse.ResponseStatus == ResponseStatus.Error)
            {
                throw PodException.BuildException(new ConnectionErrorException());
            }

            throw PodException.BuildException(new UnexpectedException());
        }
    }
}
