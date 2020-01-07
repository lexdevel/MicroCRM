using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace MicroCRM
{
    internal static class Program
    {
        internal static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost
                .CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        internal static void Main(string[] args) =>
            CreateWebHostBuilder(args).Build().Run();
    }
}
