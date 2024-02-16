using Bogus.Extensions.Brazil;
using FastFeet.Application.Users.CreateUserCommand;
using FastFeet.Domain.Enums;
using FastFeet.Test.Unit.Commons;

namespace FastFeet.Test.Unit.Application.Users.CreateUserCommandTest;

public class CreateUserCommandBuilder : BuilderBase<CreateUserCommand>
{
    private readonly string _name = FakerSingleton.GetInstance().Faker.Person.FullName;
    private readonly string _email = FakerSingleton.GetInstance().Faker.Person.Email;
    private readonly string _password = FakerSingleton.GetInstance().Faker.Random.Word();
    private readonly string _taxId = FakerSingleton.GetInstance().Faker.Person.Cpf();
    private UserType _userType = UserType.Customer;

    public override CreateUserCommand Build()
        => new(_name, _email, _password, _taxId, _userType);

    public CreateUserCommandBuilder WithUserType(UserType userType)
    {
        _userType = userType;
        return this;
    }
}