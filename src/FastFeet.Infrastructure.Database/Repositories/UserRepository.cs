using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Dapper;
using FastFeet.Domain.Entities;
using FastFeet.Domain.Interfaces.Repository;
using FastFeet.Infrastructure.Database.Snapshots;

namespace FastFeet.Infrastructure.Database.Repositories;

[ExcludeFromCodeCoverage]
internal sealed class UserRepository : IUserRepository
{
    private readonly IDbConnection _dbConnection;

    public UserRepository(IDbConnection dbConnection)
        => _dbConnection = dbConnection;

    public async Task<bool> CheckExistsEmailAsync(string email, CancellationToken cancellationToken)
    {
        var filter = " email = @email";

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
        var filter = " taxId = @taxId";

        var sqlRaw = new StringBuilder(ExistsEmailPartialCommand).Append(filter).ToString();

        var command = new CommandDefinition(
            commandText: sqlRaw,
            parameters: new { taxId },
            cancellationToken: cancellationToken);

        var total = await _dbConnection.ExecuteScalarAsync<int>(command);

        return total > 0;
    }

    public async Task<bool> SaveAsync(User user, CancellationToken cancellationToken)
    {
        var snapshot = UserSnapshot.ToSnapshot(user);

        var command = new CommandDefinition(
            commandText: InsertCommand,
            parameters: new
            {
                id = snapshot.Id,
                name = snapshot.Name,
                email = snapshot.UserEmail,
                password = snapshot.Password,
                taxId = snapshot.TaxId,
                userType = snapshot.UserType,
                createdAt = snapshot.CreatedAt,
            },
            cancellationToken: cancellationToken);

        var rowsAffected = await _dbConnection.ExecuteAsync(command);

        return rowsAffected > 0;
    }

    public async Task<User?> FindByIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        var command = new CommandDefinition(
            commandText: FindByIdCommnad,
            parameters: new
            {
                id = userId.ToString()
            },
            cancellationToken: cancellationToken);

        var user = await _dbConnection.QueryFirstOrDefaultAsync<UserSnapshot>(command);

        if (user is null)
            return null;

        return user.FromUser();
    }

    #region [Private Methods]

    private static string FindByIdCommnad
        => "SELECT * FROM public.users WHERE id = @id";

    private static string ExistsEmailPartialCommand
        => "SELECT COUNT(*) AS TOTAL FROM public.users WHERE";

    private static string InsertCommand
        => @"INSERT INTO public.users
                (id, name, email, password, taxId, userType, createdAt)
             VALUES
                (@id, @name, @email, @password, @taxId, @userType, @createdAt)";
    #endregion
}