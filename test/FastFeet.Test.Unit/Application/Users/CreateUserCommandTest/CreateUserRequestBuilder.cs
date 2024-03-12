using Bogus.Extensions.Brazil;
using FastFeet.Application.Users.CreateUserCommand;
using FastFeet.Domain.Enums;
using FastFeet.Test.Unit.Commons;

namespace FastFeet.Test.Unit.Application.Users.CreateUserCommandTest;

public class CreateUserRequestBuilder : BuilderBase<CreateUserRequest>
{
    private string _name = FakerSingleton.GetInstance().Faker.Person.FullName;
    private string _email = FakerSingleton.GetInstance().Faker.Person.Email;
    private readonly string _password = FakerSingleton.GetInstance().Faker.Random.Word();
    private readonly string _taxId = FakerSingleton.GetInstance().Faker.Person.Cpf(includeFormatSymbols: false);
    private UserType _userType = UserType.Customer;

    public override CreateUserRequest Build()
        => new(_name, _email, _password, _taxId, _userType);

    public CreateUserRequestBuilder WithUserType(UserType userType)
    {
        _userType = userType;
        return this;
    }

    public CreateUserRequestBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public CreateUserRequestBuilder WithEmail(string email)
    {
        _email = email;
        return this;
    }
}