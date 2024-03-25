using Bogus.Extensions.Brazil;
using FastFeet.Domain.Entities;
using FastFeet.Domain.Enums;
using FastFeet.Domain.ValueObjects;
using FastFeet.Test.Unit.Commons;
using FluentAssertions;

namespace FastFeet.Test.Unit.Domain.Entities;

public class UserTest
{
    [Fact]
    public void CreateUser_WithValidValues_ShouldReturnValidResult()
    {
        //Arrange
        Guid id = Guid.NewGuid();
        string name = FakerSingleton.GetInstance().Faker.Person.FirstName;
        var email = Email.Create(FakerSingleton.GetInstance().Faker.Person.Email);
        string password = FakerSingleton.GetInstance().Faker.Internet.Password();
        string taxId = FakerSingleton.GetInstance().Faker.Person.Cpf();
        UserType userType = FakerSingleton.GetInstance().Faker.Random.Enum<UserType>();

        //Act
        var user = User.Factory(
            name,
            email: email.Value,
            password,
            taxId,
            userType,
            id: id);

        //Assert
        user.IsSuccess.Should().BeTrue();
        user.IsFailure.Should().BeFalse();
    }

    [Fact]
    public void CreateUser_WithInvalidValues_ShouldReturnIsFailure()
    {
        //Arrange
        Guid id = Guid.NewGuid();
        string name = string.Empty;
        var email = Email.Create(FakerSingleton.GetInstance().Faker.Person.Email);
        string password = FakerSingleton.GetInstance().Faker.Internet.Password();
        string taxId = FakerSingleton.GetInstance().Faker.Person.Cpf();
        UserType type = FakerSingleton.GetInstance().Faker.Random.Enum<UserType>();

        //Act
        var user = User.Factory(
            name,
            email: email.Value,
            password,
            taxId,
            type,
            id: id);

        //Assert
        user.IsSuccess.Should().BeFalse();
        user.IsFailure.Should().BeTrue();
    }

    [Theory]
    [InlineData(UserType.Customer, false)]
    [InlineData(UserType.Deliveryman, true)]
    public void ExecuteMethodIsDeliveryMan_WithValidValues_ShouldReturnValidResult(UserType userType, bool isDeliveryMan)
    {
        //Arrange
        Guid id = Guid.NewGuid();
        string name = FakerSingleton.GetInstance().Faker.Person.FirstName;
        var email = Email.Create(FakerSingleton.GetInstance().Faker.Person.Email);
        string password = FakerSingleton.GetInstance().Faker.Internet.Password();
        string taxId = FakerSingleton.GetInstance().Faker.Person.Cpf();
        UserType type = userType;

        //Act
        var user = User.Factory(
            name,
            email: email.Value,
            password,
            taxId,
            type,
            id: id).Value;

        //Assert
        user.IsDeliveryMan().Should().Be(isDeliveryMan);
    }

    [Theory]
    [InlineData(false, false)]
    [InlineData(true, true)]
    public void ExecuteMethodUpdateActive_WithValidValues_ShouldReturnNewActiveState(bool active, bool newActiveState)
    {
        //Arrange
        Guid id = Guid.NewGuid();
        string name = FakerSingleton.GetInstance().Faker.Person.FirstName;
        var email = Email.Create(FakerSingleton.GetInstance().Faker.Person.Email);
        string password = FakerSingleton.GetInstance().Faker.Internet.Password();
        string taxId = FakerSingleton.GetInstance().Faker.Person.Cpf();
        UserType type = FakerSingleton.GetInstance().Faker.Random.Enum<UserType>();

        var user = User.Factory(
            name,
            email: email.Value,
            password,
            taxId,
            type,
            id: id).Value;

        //Act
        user.UpdateActive(active);

        //Assert
        user.IsActive().Should().Be(newActiveState);
    }
}