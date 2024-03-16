using FastFeet.Infrastructure.Database;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace FastFeet.Api;

public static class ApiDependency
{
    public static IServiceCollection AddCustomHealthChecks(this IServiceCollection services)
    {
        services.AddHealthChecks()
            .AddCheck<DatabaseHealthCheck>("database", HealthStatus.Unhealthy);

        return services;
    }
}
