using FastFeet.Domain.Entities;
using FastFeet.Domain.Enums;
using FastFeet.Test.Unit.Commons.Builders;
using FluentAssertions;

namespace FastFeet.Test.Unit.Domain.Entities;
public class DeliveryTest
{
    [Fact]
    public void CreateDelivery_WithValidValues_ShouldReturnValidResult()
    {
        //Arrange
        var shippingAddress = new ShippingAddressBuilder().Build();

        //Act
        var delivery = Delivery.Factory(shippingAddress);

        //Assert
        delivery.IsFailure.Should().BeFalse();
        delivery.IsSuccess.Should().BeTrue();
        delivery.Value.ShippingAddress.Should().BeEquivalentTo(shippingAddress);
        delivery.Value.Status.Should().Be(DeliveryStatus.NotAvailable);
        delivery.Value.DeliveryMan.Should().BeNull();
        delivery.Value.ModifiedOnUtc.Should().BeNull();
        delivery.Value.CreatedOnUtc.Should().BeBefore(DateTime.UtcNow);
    }

    [Fact]
    public void ExecuteMethodToAvailable_WithValidStatus_ShouldReturnSuccess()
    {
        //Arrange
        var delivery = new DeliveryBuilder().Build();

        //Act
        delivery.ToAvailable();

        //Assert
        delivery.Status.Should().Be(DeliveryStatus.Available);
    }

    [Fact]
    public void ExecuteMethodToCancelled_WithValidStatus_ShouldReturnSuccess()
    {
        //Arrange
        var delivery = new DeliveryBuilder().Build();

        //Act
        delivery.ToCancelled();

        //Assert
        delivery.Status.Should().Be(DeliveryStatus.Cancelled);
    }

    [Fact]
    public void ExecuteMethodToCompleted_WithValidStatus_ShouldReturnSuccess()
    {
        //Arrange
        var delivery = new DeliveryBuilder().Build();

        //Act
        delivery.ToCompleted();

        //Assert
        delivery.Status.Should().Be(DeliveryStatus.Completed);
    }

    [Fact]
    public void ExecuteMethodToAccept_WithValidUser_ShouldReturnSuccess()
    {
        //Arrange
        var deliveryMan = new UserBuilder().WithUserType(UserType.Deliveryman)
            .Build();

        var delivery = new DeliveryBuilder().Build();

        //Act
        delivery.ToAccept(deliveryMan);

        //Assert
        delivery.Status.Should().Be(DeliveryStatus.Completed);
    }
}