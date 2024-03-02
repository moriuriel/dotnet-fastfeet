using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace FastFeet.Application;

public static class ApplicationDependency
{
    public static IServiceCollection AddCustomMediatr(this IServiceCollection services)
    {
        services.AddMediatR(_ => _.RegisterServicesFromAssemblies(
            assemblies: Assembly.GetExecutingAssembly()));

        return services;
    }
}