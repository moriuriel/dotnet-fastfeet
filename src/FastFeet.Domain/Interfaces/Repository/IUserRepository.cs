using FastFeet.Domain.Entities;

namespace FastFeet.Domain.Interfaces.Repository;

public interface IUserRepository
{
    Task<bool> CheckHasEmail(string email);
    Task<bool> CheckHasTaxId(string taxId);
    Task<bool> Create(User user);
}