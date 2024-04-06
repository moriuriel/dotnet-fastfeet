using FastFeet.Domain.Entities;
using FastFeet.Domain.Enums;
using FastFeet.Domain.ValueObjects;

namespace FastFeet.Infrastructure.Database.Snapshots;

public sealed class UserSnapshot
{
    public string Id { get; init; } = string.Empty;

    public string Name { get; init; } = string.Empty;

    public string UserEmail { get; init; } = string.Empty;

    public string Password { get; init; } = string.Empty;

    public string TaxId { get; init; } = string.Empty;

    public bool Active { get; init; }

    public UserType UserType { get; init; }

    public DateTime CreatedAt { get; init; }

    public DateTime? UpdatedAt { get; init; }

    public static UserSnapshot ToSnapshot(User user)
        => new UserSnapshot
        {
            Id = user.Id.ToString(),
            UserEmail = user.Email.Value,
            Name = user.Name,
            Password = user.Password,
            CreatedAt = user.CreatedAt,
            TaxId = user.TaxId,
            Active = user.Active,
            UserType = user.UserType,
            UpdatedAt = user.UpdatedAt
        };

    public User FromUser()
        => User.Factory(
            name: Name,
            email: Email.Create(UserEmail).Value,
            password: Password,
            taxId: TaxId,
            userType: UserType,
            active: Active,
            id: Guid.Parse(Id)).Value;
}