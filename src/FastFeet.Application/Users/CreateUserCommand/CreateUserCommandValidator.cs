using FluentValidation;

namespace FastFeet.Application.Users.CreateUserCommand
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
	{
		public CreateUserCommandValidator()
		{
            RuleFor(_ => _.Name)
            .NotEmpty()
            .MinimumLength(3);

            RuleFor(_ => _.Email)
            .NotEmpty()
            .EmailAddress();

            RuleFor(_ => _.Password)
           .NotEmpty()
           .MinimumLength(3);
        }
	}
}

