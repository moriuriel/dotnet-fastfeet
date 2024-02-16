using FastFeet.Domain.Enums;
using FastFeet.Domain.Commons;
using FluentValidation;
using FluentValidation.Results;

namespace FastFeet.Domain.Entities;

public sealed class User : AggregateRoot, IValidationDomain
{
    public User(
        Guid? id,
        string name,
        string email,
        string password,
        string taxId,
        bool active,
        UserType type,
        DateTime createdAt) : base(id: id ?? Guid.NewGuid())
    {
        Name = name;
        Email = email;
        Password = password;
        TaxId = taxId;
        Active = active;
        Type = type;
        CreatedAt = createdAt;
    }

    public static Result<User> Factory(
        string name,
        string email,
        string password,
        string taxId,
        UserType type,
        bool active = true,
        Guid? id = null)
    {
        var entity = new User(
            id,
            name,
            email,
            password,
            taxId,
            active,
            type,
            createdAt: DateTime.Now);

        var validationResult = entity.GetValidationResult();

        if (!validationResult.IsValid)
            return Result.Failure<User>(errors: DomainError.GetErrors(validationResult.Errors));

        return entity;
    }

    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public string TaxId { get; private set; }
    public bool Active { get; private set; }
    public UserType Type { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public ValidationResult GetValidationResult()
        => new UserValidator().Validate(this);

    public bool IsDeliveryMan()
        => Type == UserType.Deliveryman;

    public bool IsActive()
        => Active;

    public void UpdateActive(bool active)
    {
        Active = active;
        UpdatedAt = DateTime.Now;
    }
}

internal sealed class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(_ => _.Id)
            .NotEmpty();

        RuleFor(_ => _.Type)
            .IsInEnum();

        RuleFor(_ => _.Name)
            .NotEmpty()
            .MinimumLength(3);

        RuleFor(_ => _.Active)
            .NotEmpty();

        RuleFor(_ => _.Email)
            .EmailAddress();

        RuleFor(_ => _.TaxId)
            .NotEmpty();

        RuleFor(_ => _.Password)
            .NotEmpty();
    }
}