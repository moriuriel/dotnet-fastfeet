using FastFeet.Domain.Entities;
using FastFeet.Domain.ValueObjects;

namespace FastFeet.Test.Unit.Commons.Builders;

public class DeliveryBuilder : BuilderBase<Delivery>
{
    public DeliveryBuilder()
    {
        _shippingAddress = new ShippingAddressBuilder().Build();
    }

    private readonly ShippingAddress _shippingAddress;

    public override Delivery Build()
    => Delivery.Factory(_shippingAddress).Value;
}
