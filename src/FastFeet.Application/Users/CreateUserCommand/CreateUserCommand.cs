using FastFeet.Application.Commons.Command;
using FastFeet.Application.Commons.Response;
using FastFeet.Domain.Enums;
using MediatR;

namespace FastFeet.Application.Users.CreateUserCommand;

public sealed record CreateUserCommand(
    string Name,
    string Email,
    string Password,
    string TaxId,
    UserType UserType) : CommandBase, IRequest<Response>
{
    public override bool IsValid()
    {
        ValidationResult = new CreateUserCommandValidator().Validate(this);
        return ValidationResult.IsValid;
    }
}
