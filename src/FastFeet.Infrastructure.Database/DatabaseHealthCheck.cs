using System.Data;
using Dapper;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace FastFeet.Infrastructure.Database;

public sealed class DatabaseHealthCheck : IHealthCheck
{
    private readonly IDbConnection _dbConnection;

    public DatabaseHealthCheck(IDbConnection dbConnection)
        => _dbConnection = dbConnection;

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            await _dbConnection.ExecuteScalarAsync("SELECT 1");

            return HealthCheckResult.Healthy();
        } catch(Exception e)
        {
            return HealthCheckResult.Unhealthy(exception: e);
        }
    }
}


