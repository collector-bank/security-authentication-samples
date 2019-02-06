using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace CollectorBank.Security.Authentication.Samples.OpenIDConnectWebClientCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging(builder => {
                    builder.AddConsole();
                    builder.AddDebug();
                }).UseStartup<Startup>();
    }
}