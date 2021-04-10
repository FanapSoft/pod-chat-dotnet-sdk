using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace POD_Demo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var weHost = CreateWebHostBuilder(args).Build();
            new ChatMethodInvoke();
            weHost.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
