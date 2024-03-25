using FastFeet.Domain.Commons;
using FluentValidation;
using FluentValidation.Results;

namespace FastFeet.Domain.ValueObjects;

public sealed class Email : IValidationDomain
{
    private Email(string value)
        => Value = value;

    public string Value { get; }

    public static Result<Email> Create(string email)
    {
        var valueObject = new Email(email);

        var validationResult = valueObject.GetValidationResult();

        if (!validationResult.IsValid)
            return Result.Failure<Email>(errors: DomainError.GetErrors(validationResult.Errors));

        return valueObject;
    }

    public ValidationResult GetValidationResult()
        => new EmailValidator().Validate(this);
}

internal sealed class EmailValidator : AbstractValidator<Email>
{
    public EmailValidator()
    {
        RuleFor(_ => _.Value)
            .EmailAddress();
    }
}