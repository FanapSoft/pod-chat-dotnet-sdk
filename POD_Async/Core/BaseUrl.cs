
namespace POD_Async.Core
{
    public static class BaseUrl
    {
        public static string SsoAddress = "https://accounts.pod.ir";

        public static string PlatformAddress = CoreConfig.ServerType == ServerType.SandBox
            ? "http://sandbox.pod.ir:8080"
            : "https://api.pod.ir/srv/core";

        public static string PrivateCallAddress = "https://pay.pod.ir";

        public static string FileServerAddress = CoreConfig.ServerType == ServerType.SandBox
            ? "http://sandbox.pod.ir:8080"
            : "https://core.pod.ir";

        public static string ServiceCallAddress = CoreConfig.ServerType == ServerType.SandBox
            ? "http://sandbox.pod.ir/srv/basic-platform/nzh/doServiceCall/"
            : "https://api.pod.ir/srv/core/nzh/doServiceCall";

        public static string PodSpaceFileAddress = "https://podspace.pod.ir";

    }
}
