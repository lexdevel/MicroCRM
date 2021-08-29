using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace MicroCRM
{
    internal static class Program
    {
        internal static void Main(string[] args) =>
            WebHost
                .CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build()
                .Run();
    }
}
