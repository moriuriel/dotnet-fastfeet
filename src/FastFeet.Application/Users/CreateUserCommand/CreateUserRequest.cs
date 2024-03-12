using FastFeet.Domain.Enums;

namespace FastFeet.Application.Users.CreateUserCommand;

public sealed record CreateUserRequest(string Name,
    string Email,
    string Password,
    string TaxId,
    UserType UserType);