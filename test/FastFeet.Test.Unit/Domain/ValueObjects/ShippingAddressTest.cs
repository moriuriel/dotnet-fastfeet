using FastFeet.Domain.ValueObjects;
using FastFeet.Test.Unit.Commons;
using FluentAssertions;

namespace FastFeet.Test.Unit.Domain.ValueObjects;

public class ShippingAddressTest
{
    [Fact]
    public void CreateShppingAddress_WithValidValues_ShouldReturnSuccess()
    {
        string number = FakerSingleton.GetInstance().Faker.Address.BuildingNumber();
        string street = FakerSingleton.GetInstance().Faker.Address.StreetName();
        string city = FakerSingleton.GetInstance().Faker.Address.City();
        string state = FakerSingleton.GetInstance().Faker.Address.State();
        string zipCode = FakerSingleton.GetInstance().Faker.Address.ZipCode();
        string neighborhood = FakerSingleton.GetInstance().Faker.Address.CardinalDirection();
        string reference = FakerSingleton.GetInstance().Faker.Address.SecondaryAddress();

        var shippingAddress = ShippingAddress.Create(
            number,
            reference,
            city,
            neighborhood,
            state,
            zipCode,
            street);

        shippingAddress.IsFailure.Should().BeFalse();
        shippingAddress.IsSuccess.Should().BeTrue();
        shippingAddress.Value.Neighborhood.Should().Be(neighborhood);
        shippingAddress.Value.Street.Should().Be(street);
        shippingAddress.Value.City.Should().Be(city);
        shippingAddress.Value.State.Should().Be(state);
        shippingAddress.Value.ZipCode.Should().Be(zipCode);
        shippingAddress.Value.Reference.Should().Be(reference);
    }
}
