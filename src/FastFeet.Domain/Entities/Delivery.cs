using FastFeet.Domain.Commons;
using FastFeet.Domain.Enums;
using FastFeet.Domain.ValueObjects;
using FluentValidation;
using FluentValidation.Results;

namespace FastFeet.Domain.Entities;
public class Delivery : Entity, IValidationDomain
{
    private Delivery(
        ShippingAddress shippingAddress,
        Guid? id = null) : base(id: id ?? Guid.NewGuid())
    {
        ShippingAddress = shippingAddress;
        CreatedOnUtc = DateTime.UtcNow;
        Status = DeliveryStatus.NotAvailable;
    }

    public ShippingAddress ShippingAddress { get; }

    public User DeliveryMan { get; private set; } = default!;

    public DateTime CreatedOnUtc { get; }

    public DateTime? ModifiedOnUtc { get; private set; }

    public DeliveryStatus Status { get; private set; }

    public bool IsDeliveryAvailable
        => Status == DeliveryStatus.Available;

    public static Result<Delivery> Factory(
        ShippingAddress shippingAddress,
        Guid? id = null)
    {
        var entity = new Delivery(
            shippingAddress,
            id);

        var validationResult = entity.GetValidationResult();

        if (!validationResult.IsValid)
            return Result.Failure<Delivery>(errors: DomainError.GetErrors(validationResult.Errors));

        return entity;
    }

    public ValidationResult GetValidationResult()
        => new DeliveryValidator().Validate(this);

    internal void ToAvailable()
    {
        ModifiedOnUtc = DateTime.UtcNow;
        Status = DeliveryStatus.Available;
    }

    internal void ToCancelled()
    {
        ModifiedOnUtc = DateTime.UtcNow;
        Status = DeliveryStatus.Cancelled;
    }

    internal void ToAccept(User deliveryMan)
    {
        DeliveryMan = deliveryMan;
        ModifiedOnUtc = DateTime.UtcNow;
        Status = DeliveryStatus.Accepted;
    }

    internal void ToCompleted()
    {
        ModifiedOnUtc = DateTime.UtcNow;
        Status = DeliveryStatus.Completed;
    }
}

internal sealed class DeliveryValidator : AbstractValidator<Delivery>
{
    public DeliveryValidator()
    {
        RuleFor(_ => _.ShippingAddress)
            .NotNull();
        RuleFor(_ => _.Id)
            .NotEmpty();
    }
}