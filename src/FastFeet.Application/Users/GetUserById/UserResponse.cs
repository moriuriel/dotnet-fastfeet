using FastFeet.Domain.Enums;

namespace FastFeet.Application.Users.GetUserById;

public sealed record UserResponse(
    Guid? Id,
    string Name,
    bool Active,
    UserType UserType,
    DateTime CreatedAt);
