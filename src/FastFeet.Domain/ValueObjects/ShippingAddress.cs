using FastFeet.Domain.Commons;
using FluentValidation;
using FluentValidation.Results;

namespace FastFeet.Domain.ValueObjects;

public sealed class ShippingAddress : IValidationDomain
{
    private ShippingAddress(
        string number,
        string reference,
        string city,
        string neighborhood,
        string state,
        string zipCode,
        string street)
    {
        Number = number;
        Reference = reference;
        City = city;
        Neighborhood = neighborhood;
        State = state;
        ZipCode = zipCode;
        Street = street;
    }

    public string Number { get; }
    public string Reference { get; }
    public string City { get; }
    public string Neighborhood { get; }
    public string State { get; }
    public string ZipCode { get; }
    public string Street { get; }

    public static Result<ShippingAddress> Create(
        string number,
        string reference,
        string city,
        string neighborhood,
        string state,
        string zipCode,
        string street)
    {
        var valueObject = new ShippingAddress(
            number,
            reference,
            city,
            neighborhood,
            state,
            zipCode,
            street);

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
            .NotEmpty();
        RuleFor(_ => _.Neighborhood)
            .NotEmpty();
        RuleFor(_ => _.City)
            .NotEmpty();
        RuleFor(_ => _.ZipCode)
            .NotEmpty();
        RuleFor(_ => _.State)
            .NotEmpty();
        RuleFor(_ => _.Street)
            .NotEmpty();
        RuleFor(_ => _.Reference)
            .NotEmpty()
            .When(_ => string.IsNullOrWhiteSpace(_.Reference));
    }
}