using System.Data;
using System.Diagnostics.CodeAnalysis;
using FastFeet.Domain.Entities;
using FastFeet.Domain.Interfaces.Repository;

namespace FastFeet.Infrastructure.Database.Repositories;

[ExcludeFromCodeCoverage]
internal sealed class UserRepository : IUserRepository
{
    //private readonly IDbConnection _dbConnection;

    //public UserRepository(IDbConnection dbConnection)
    //    => _dbConnection = dbConnection;

    public Task<bool> CheckHasEmail(string email)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CheckHasTaxId(string taxId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Create(User user)
    {
        throw new NotImplementedException();
    }
}


