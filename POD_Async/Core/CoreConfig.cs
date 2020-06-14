
namespace POD_Async.Core
{
    public static class CoreConfig
    {
        /// <summary>
        /// Production : تمام سرویس ها بر روی سرور اصلی اجرا می شوند
        /// SandBox : تمام سرویس ها بر روی سرور تست اجرا می شوند
        /// </summary>
        public static ServerType ServerType { get; set; }


        /// <summary>
        /// توکن های داخلی : 0
        /// توکن های دریافت شده از SSO  : 1
        /// </summary>
        public static int TokenIssuer { get; set; } = 1;
    }
}
