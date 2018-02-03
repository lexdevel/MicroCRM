using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace MicroCRM
{
    /// <summary>
    /// The application entry point class.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// Builds the web host.
        /// </summary>
        /// <param name="args">The command-line arguments.</param>
        /// <returns>The web host.</returns>
        internal static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseApplicationInsights()
                .UseStartup<Startup>()
                .Build();

        /// <summary>
        /// The application entry point.
        /// </summary>
        /// <param name="args">The command-line arguments.</param>
        internal static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }
    }
}
