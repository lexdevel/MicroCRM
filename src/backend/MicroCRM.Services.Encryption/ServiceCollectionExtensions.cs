using Microsoft.Extensions.DependencyInjection;
using MicroCRM.Services.Encryption.Internal;

namespace MicroCRM.Services.Encryption
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEncryptionService(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddScoped<IEncryptionService, EncryptionService>();
        }
    }
}
