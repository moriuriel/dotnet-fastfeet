using FastFeet.Application.Commons.Command;
using FastFeet.Application.Commons.Response;
using FluentValidation;
using MediatR;

namespace FastFeet.Application.Users.CreateUserCommand;

public sealed record CreateUserCommand(
    Guid IdempotencyKey,
    CreateUserRequest user) : CommandBase, IRequest<Response>
{
    public override bool IsValid()
    {
        ValidationResult = new CreateUserCommandValidator().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(_ => _.IdempotencyKey)
        .NotEmpty().Must(idempotencyKey => Guid.TryParse(idempotencyKey.ToString(), out var newGuid));

        RuleFor(_ => _.user)
            .SetValidator(new CreateUserRequestValidator());
    }
}