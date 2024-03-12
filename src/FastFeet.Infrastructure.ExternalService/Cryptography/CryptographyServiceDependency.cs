using Microsoft.Extensions.DependencyInjection;

namespace FastFeet.Infrastructure.ExternalService.Cryptography;

public static class CryptographyServiceDependency
{
    public static IServiceCollection AddCryptographyService(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ICryptographyService, CryptographyService>();
        return serviceCollection;
    }

}