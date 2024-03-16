using System.Reflection;
using FastFeet.Application.Commons.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace FastFeet.Application;

public static class ApplicationDependency
{
    public static IServiceCollection AddCustomMediatr(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(assemblies: Assembly.GetExecutingAssembly());
            config.AddOpenBehavior(typeof(RequestLoggingPipelineBehavior<,>));
        });

        return services;
    }
}