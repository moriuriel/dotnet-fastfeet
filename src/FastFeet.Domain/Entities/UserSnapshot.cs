using FastFeet.Domain.Enums;

namespace FastFeet.Domain.Entities;

public sealed class UserSnapshot
{
    public string Id { get; init; } = string.Empty;

    public string Name { get; init; } = string.Empty;

    public string Email { get; init; } = string.Empty;

    public string Password { get; init; } = string.Empty;

    public string TaxId { get; init; } = string.Empty;

    public bool Active { get; init; }

    public UserType UserType { get; init; }

    public DateTime CreatedAt { get; init; }

    public DateTime? UpdatedAt { get; init; }
}