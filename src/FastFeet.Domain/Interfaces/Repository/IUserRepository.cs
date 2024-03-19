using FastFeet.Domain.Entities;

namespace FastFeet.Domain.Interfaces.Repository;

public interface IUserRepository
{
    Task<bool> CheckExistsEmailAsync(string email, CancellationToken cancellationToken);
    Task<bool> CheckExistsTaxIdAsync(string taxId, CancellationToken cancellationToken);
    Task<bool> SaveAsync(User user, CancellationToken cancellationToken);
}