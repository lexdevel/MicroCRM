using Microsoft.Extensions.DependencyInjection;
using MicroCRM.Services.Random.Internal;

namespace MicroCRM.Services.Random
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRandomService(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddScoped<IRangomService, RandomService>();
        }
    }
}
