using System.Data;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace FastFeet.Infrastructure.Database;

public static class DatabaseContext
{
    public static IServiceCollection AddDatabaseContext(this IServiceCollection services)
    {
        services.AddScoped<IDbConnection>((_) => new NpgsqlConnection(""));
        return services;
    }
}
