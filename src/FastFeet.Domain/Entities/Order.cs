using FastFeet.Domain.Commons;
using FastFeet.Domain.Enums;
using FluentValidation.Results;

namespace FastFeet.Domain.Entities;

public sealed class Order : AggregateRoot, IValidationDomain
{
    public Order(
        User owner,
        Delivery delivery,
        Guid? id = null) : base(id ?? Guid.NewGuid())
    {
        Owner = owner;
        CreatedOnUtc = DateTime.UtcNow;
        Delivery = delivery;
        Status = OrderStatus.ToDo;
    }

    public User Owner { get; private set; }
    public DateTime CreatedOnUtc { get; private set; }
    public DateTime? ModifiedOnUtc { get; private set; }
    public Delivery Delivery { get; private set; }
    public OrderStatus Status { get; private set; }

    public static Result<Order> Factory(
        User owner,
        Delivery delivery,
        Guid? id = null)
    {
        var entity = new Order(
            owner,
            delivery,
            id);

        var validationResult = entity.GetValidationResult();

        if (!validationResult.IsValid)
            return Result.Failure<Order>(errors: DomainError.GetErrors(validationResult.Errors));

        return entity;
    }

    public void ToDoing()
    {
        Status = OrderStatus.Doing;
        ModifiedOnUtc = DateTime.UtcNow;
    }

    public void ToDelivery(User deliveryMan)
    {
        Status = OrderStatus.Delivery;
        ModifiedOnUtc = DateTime.UtcNow;
        Delivery.ToAccept(deliveryMan);
    }

    public void ToCompleted()
    {
        Status = OrderStatus.Delivery;
        ModifiedOnUtc = DateTime.UtcNow;
        Delivery.ToCompleted();
    }

    public void ToCancelled()
    {
        Status = OrderStatus.Cancelled;
        ModifiedOnUtc = DateTime.UtcNow;
        Delivery.ToCancelled();
    }

    public void ToReady()
    {
        Status = OrderStatus.Ready;
        ModifiedOnUtc = DateTime.UtcNow;
        Delivery.ToAvailable();
    }

    public ValidationResult GetValidationResult()
    {
        throw new NotImplementedException();
    }
}