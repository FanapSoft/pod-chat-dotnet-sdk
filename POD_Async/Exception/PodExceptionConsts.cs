namespace POD_Async.Exception
{
    public static class PodExceptionConsts
    {
        #region Code

        public const int InvalidParameterCode = 887;
        public const int UnexpectedErrorCode = 888;
        public const int ConnectionErrorCode = 889;

        #endregion Code

        #region Message

        public const string UnexpectedErrorMessage = "Unexpected error occured.";
        public const string InvalidParameterMessage = "Invalid parameter(s).";
        public const string ConnectionErrorMessage = "Connection error occured.";

        #endregion Message
    }
}
