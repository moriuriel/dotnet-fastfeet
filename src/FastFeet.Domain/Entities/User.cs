using FastFeet.Domain.Enums;
using FastFeet.Domain.Commons;
using FluentValidation;
using FluentValidation.Results;
using FastFeet.Domain.ValueObjects;

namespace FastFeet.Domain.Entities;

public sealed class User : AggregateRoot, IValidationDomain
{
    private User()
    { }

    private User(
        Guid? id,
        string name,
        Email email,
        string password,
        string taxId,
        bool active,
        UserType userType,
        DateTime createdAt) : base(id: id ?? Guid.NewGuid())
    {
        Name = name;
        Email = email;
        Password = password;
        TaxId = taxId;
        Active = active;
        UserType = userType;
        CreatedAt = createdAt;
    }

    public static Result<User> Factory(
        string name,
        Email email,
        string password,
        string taxId,
        UserType userType,
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
            userType,
            createdAt: DateTime.Now);

        var validationResult = entity.GetValidationResult();

        if (!validationResult.IsValid)
            return Result.Failure<User>(errors: DomainError.GetErrors(validationResult.Errors));

        return entity;
    }

    public string Name { get; private set; }
    public Email Email { get; private set; }
    public string Password { get; private set; }
    public string TaxId { get; private set; }
    public bool Active { get; private set; }
    public UserType UserType { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public ValidationResult GetValidationResult()
        => new UserValidator().Validate(this);

    public bool IsDeliveryMan()
        => UserType == UserType.Deliveryman;

    public bool IsActive()
        => Active;

    public void UpdateActive(bool active)
    {
        Active = active;
        UpdatedAt = DateTime.Now;
    }

    public UserSnapshot ToUserSnapshot()
    {
        return new UserSnapshot
        {
            Id = Id.ToString(),
            Email = Email.Value,
            Name = Name,
            Password = Password,
            CreatedAt = CreatedAt,
            TaxId = TaxId,
            Active = Active,
            UserType = UserType,
            UpdatedAt = UpdatedAt
        };
    }

    public static User FromSnapshot(UserSnapshot userSnapshot)
    {
        return new User
        {
            Id = Guid.Parse(userSnapshot.Id),
            Name = userSnapshot.Name,
            Email = Email.Create(userSnapshot.Email).Value,
            Password = userSnapshot.Password,
            TaxId = userSnapshot.TaxId,
            Active = userSnapshot.Active,
            UserType = userSnapshot.UserType,
            CreatedAt = userSnapshot.CreatedAt,
            UpdatedAt = userSnapshot.UpdatedAt,
        };
    }
}
internal sealed class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(_ => _.Id)
            .NotEmpty();

        RuleFor(_ => _.UserType)
            .IsInEnum();

        RuleFor(_ => _.Name)
            .NotEmpty()
            .MinimumLength(3);

        RuleFor(_ => _.Active)
            .NotEmpty();

        RuleFor(_ => _.TaxId)
            .NotEmpty();

        RuleFor(_ => _.Password)
            .NotEmpty();
    }
}