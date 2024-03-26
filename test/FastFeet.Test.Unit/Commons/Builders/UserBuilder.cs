using Bogus.Extensions.Brazil;
using FastFeet.Domain.Entities;
using FastFeet.Domain.Enums;
using FastFeet.Domain.ValueObjects;

namespace FastFeet.Test.Unit.Commons.Builders;

public sealed class UserBuilder : BuilderBase<User>
{
    public UserBuilder()
    {
        _id = Guid.NewGuid();
        _name = FakerSingleton.GetInstance().Faker.Person.FirstName;
        _email = Email.Create(FakerSingleton.GetInstance().Faker.Person.Email).Value;
        _password = FakerSingleton.GetInstance().Faker.Internet.Password();
        _taxId = FakerSingleton.GetInstance().Faker.Person.Cpf();
        _userType = UserType.Customer;
        _active = true;
    }

    private readonly Guid _id;
    private readonly string _name;
    private readonly Email _email;
    private readonly string _password;
    private readonly string _taxId;
    private UserType _userType;
    private bool _active;

    public override User Build()
        => User.Factory(
            name: _name,
            email: _email,
            password: _password,
            taxId: _taxId,
            userType: _userType,
            id: _id).Value;

    public UserBuilder WithUserType(UserType userType)
    {
        _userType = userType;
        return this;
    }

    public UserBuilder WithActive(bool active)
    {
        _active = active;
        return this;
    }
}
