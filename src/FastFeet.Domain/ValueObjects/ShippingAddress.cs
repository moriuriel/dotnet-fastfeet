using FastFeet.Domain.Commons;
using FluentValidation;
using FluentValidation.Results;

namespace FastFeet.Domain.ValueObjects;

public class ShippingAddress : IValidationDomain
{
    private ShippingAddress(
        long number,
        string reference,
        string city,
        string neighborhood,
        string state,
        string postalCode)
    {
        Number = number;
        Reference = reference;
        City = city;
        Neighborhood = neighborhood;
        State = state;
        PostalCode = postalCode;
    }

    public long Number { get; }
    public string Reference { get; }
    public string City { get; }
    public string Neighborhood { get; }
    public string State { get; }
    public string PostalCode { get; }

    public static Result<ShippingAddress> Create(
        long number,
        string reference,
        string city,
        string neighborhood,
        string state,
        string postalCode)
    {
        var valueObject = new ShippingAddress(
            number,
            reference,
            city,
            neighborhood,
            state,
            postalCode);

        var validationResult = valueObject.GetValidationResult();

        if (!validationResult.IsValid)
            return Result.Failure<ShippingAddress>(errors: DomainError.GetErrors(validationResult.Errors));

        return valueObject;
    }

    public ValidationResult GetValidationResult()
        => new ShippingAddressValidator().Validate(this);
}

internal sealed class ShippingAddressValidator : AbstractValidator<ShippingAddress>
{
    public ShippingAddressValidator()
    {
        RuleFor(_ => _.Number)
            .NotNull();
        RuleFor(_ => _.Neighborhood)
            .NotEmpty();
        RuleFor(_ => _.City)
            .NotEmpty();
        RuleFor(_ => _.PostalCode)
            .NotEmpty();
        RuleFor(_ => _.State)
            .NotEmpty();
        RuleFor(_ => _.Reference)
            .NotEmpty()
            .When(_ => string.IsNullOrWhiteSpace(_.Reference));
    }
}