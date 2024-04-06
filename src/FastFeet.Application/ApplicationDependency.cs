using System.Reflection;
using FastFeet.Application.Commons.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace FastFeet.Application;

public static class ApplicationDependency
{
    public static IServiceCollection AddCustomMediatr(this IServiceCollection services)
    {
        services.AddMediatR(_ => _.RegisterServicesFromAssemblies(assemblies: Assembly.GetExecutingAssembly()));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestLoggingPipelineBehavior<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
        //services.AddScoped(typeof(IPipelineBehavior<,>), typeof(IdempotentCommandPipelineBehavior<,>));

        return services;
    }
}