using System.Data;
using FastFeet.Domain.Interfaces.Repository;
using FastFeet.Infrastructure.Database.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace FastFeet.Infrastructure.Database;

public static class DatabaseContext
{
    public static IServiceCollection AddDatabaseContext(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        //var connectionString = configuration.GetConnectionString("PostgreFastFeet");
        //ArgumentNullException.ThrowIfNull(nameof(connectionString));

        //services.AddScoped<IDbConnection>(_ => new NpgsqlConnection(connectionString));
        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
}