using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Dapper;
using FastFeet.Domain.Entities;
using FastFeet.Domain.Interfaces.Repository;

namespace FastFeet.Infrastructure.Database.Repositories;

[ExcludeFromCodeCoverage]
internal sealed class UserRepository : IUserRepository
{
    private readonly IDbConnection _dbConnection;

    public UserRepository(IDbConnection dbConnection)
        => _dbConnection = dbConnection;

    public async Task<bool> CheckExistsEmailAsync(string email, CancellationToken cancellationToken)
    {
        var filter = "email = @email";

        var sqlRaw = new StringBuilder(ExistsEmailPartialCommand).Append(filter).ToString();

        var command = new CommandDefinition(
            commandText: sqlRaw,
            parameters: new { email },
            cancellationToken: cancellationToken);

        var total = await _dbConnection.ExecuteScalarAsync<int>(command);

        return total > 0;
    }

    public async Task<bool> CheckExistsTaxIdAsync(string taxId, CancellationToken cancellationToken)
    {
        var filter = "email = @taxId";

        var sqlRaw = new StringBuilder(ExistsEmailPartialCommand).Append(filter).ToString();

        var command = new CommandDefinition(
            commandText: sqlRaw,
            parameters: new { taxId },
            cancellationToken: cancellationToken);

        var total = await _dbConnection.ExecuteScalarAsync<int>(command);

        return total > 0;
    }

    public Task<bool> SaveAsync(User user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    #region [Private Methods]
    private static string ExistsEmailPartialCommand
        => "SELECT COUNT(*) AS TOTAL WHERE";
    #endregion
}