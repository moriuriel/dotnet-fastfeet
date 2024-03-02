using Bogus.Extensions.Brazil;
using FastFeet.Application.Users.CreateUserCommand;
using FastFeet.Domain.Enums;
using FastFeet.Test.Unit.Commons;

namespace FastFeet.Test.Unit.Application.Users.CreateUserCommandTest;

public class CreateUserCommandBuilder : BuilderBase<CreateUserCommand>
{
    private string _name = FakerSingleton.GetInstance().Faker.Person.FullName;
    private string _email = FakerSingleton.GetInstance().Faker.Person.Email;
    private readonly string _password = FakerSingleton.GetInstance().Faker.Random.Word();
    private readonly string _taxId = FakerSingleton.GetInstance().Faker.Person.Cpf(includeFormatSymbols: false);
    private UserType _userType = UserType.Customer;

    public override CreateUserCommand Build()
        => new(_name, _email, _password, _taxId, _userType);

    public CreateUserCommandBuilder WithUserType(UserType userType)
    {
        _userType = userType;
        return this;
    }

    public CreateUserCommandBuilder With(string name)
    {
        _name = name;
        return this;
    }

    public CreateUserCommandBuilder WithEmail(string email)
    {
        _email = email;
        return this;
    }
}