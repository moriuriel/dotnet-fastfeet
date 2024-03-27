using FastFeet.Domain.Commons;
using FastFeet.Domain.Enums;
using FastFeet.Domain.ValueObjects;
using FluentValidation;
using FluentValidation.Results;

namespace FastFeet.Domain.Entities;
public class Delivery : AggregateRoot, IValidationDomain
{
    private Delivery(
        ShippingAddress shippingAddress,
        Guid customerId,
        Guid? id = null) : base(id: id ?? Guid.NewGuid())
    {
        ShippingAddress = shippingAddress;
        CustomerId = customerId;
        CreatedAtUtc = DateTime.UtcNow;
        Status = DeliveryStatus.Available;
    }

    public ShippingAddress ShippingAddress { get; }

    public Guid CustomerId { get; }

    public Guid DeliveryManId { get; private set; }

    public DateTime CreatedAtUtc { get; }

    public DateTime? ModifiedOnUtc { get; private set; }

    public DeliveryStatus Status { get; private set; }

    public bool IsDeliveryAvailable
        => Status == DeliveryStatus.Available;

    public Result<Delivery> Factory(
        ShippingAddress shippingAddress,
        Guid customerId,
        Guid? id = null)
    {
        var entity = new Delivery(
            shippingAddress,
            customerId,
            id);

        var validationResult = entity.GetValidationResult();

        if (!validationResult.IsValid)
            return Result.Failure<Delivery>(errors: DomainError.GetErrors(validationResult.Errors));

        return entity;
    }

    public ValidationResult GetValidationResult()
        => new DeliveryValidator().Validate(this);

    public void Accept(Guid deliveryManId)
    {
        DeliveryManId = deliveryManId;
        ModifiedOnUtc = DateTime.UtcNow;
        Status = DeliveryStatus.Accepted;
    }

    public void Cancel()
    {
        ModifiedOnUtc = DateTime.UtcNow;
        Status = DeliveryStatus.Canceled;
    }

    public void Complete()
    {
        ModifiedOnUtc = DateTime.UtcNow;
        Status = DeliveryStatus.Completed;
    }
}

internal sealed class DeliveryValidator : AbstractValidator<Delivery>
{
    public DeliveryValidator()
    {
        RuleFor(_ => _.CustomerId)
            .NotNull();
        RuleFor(_ => _.ShippingAddress)
            .NotNull();
        RuleFor(_ => _.Id)
            .NotEmpty();
    }
}