using FastFeet.Domain.Entities;

namespace FastFeet.Domain.Interfaces.Repository;

public interface IUserRepository
{
    Task<User> FindByIdAsync(Guid userId, CancellationToken cancellationToken);
    Task<bool> CheckExistsEmailAsync(string email, CancellationToken cancellationToken);
    Task<bool> CheckExistsTaxIdAsync(string taxId, CancellationToken cancellationToken);
    Task<bool> SaveAsync(User user, CancellationToken cancellationToken);
}