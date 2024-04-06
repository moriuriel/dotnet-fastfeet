using FastFeet.Domain.ValueObjects;

namespace FastFeet.Test.Unit.Commons.Builders;

public class ShippingAddressBuilder : BuilderBase<ShippingAddress>
{
    public ShippingAddressBuilder()
    {
        _number = FakerSingleton.GetInstance().Faker.Address.BuildingNumber();
        _street = FakerSingleton.GetInstance().Faker.Address.StreetName();
        _city = FakerSingleton.GetInstance().Faker.Address.City();
        _state = FakerSingleton.GetInstance().Faker.Address.State();
        _zipCode = FakerSingleton.GetInstance().Faker.Address.ZipCode();
        _neighborhood = FakerSingleton.GetInstance().Faker.Address.CardinalDirection();
        _reference = FakerSingleton.GetInstance().Faker.Address.SecondaryAddress();
    }

    private readonly string _number = FakerSingleton.GetInstance().Faker.Address.BuildingNumber();
    private readonly string _street = FakerSingleton.GetInstance().Faker.Address.StreetName();
    private readonly string _city = FakerSingleton.GetInstance().Faker.Address.City();
    private readonly string _state = FakerSingleton.GetInstance().Faker.Address.State();
    private readonly string _zipCode = FakerSingleton.GetInstance().Faker.Address.ZipCode();
    private readonly string _neighborhood = FakerSingleton.GetInstance().Faker.Address.CardinalDirection();
    private readonly string _reference = FakerSingleton.GetInstance().Faker.Address.SecondaryAddress();

    public override ShippingAddress Build()
        => ShippingAddress.Create(
            _number,
            _reference,
            _city,
            _neighborhood,
            _state,
            _zipCode,
            _street).Value;
}
