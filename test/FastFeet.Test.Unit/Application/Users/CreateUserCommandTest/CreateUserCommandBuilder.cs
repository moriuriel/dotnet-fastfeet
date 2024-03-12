using FastFeet.Application.Users.CreateUserCommand;
using FastFeet.Test.Unit.Commons;

namespace FastFeet.Test.Unit.Application.Users.CreateUserCommandTest;

public class CreateUserCommandBuilder : BuilderBase<CreateUserCommand>
{
    public CreateUserCommandBuilder()
    {
        _user = new CreateUserRequestBuilder().Build();
        _idempotencyKey = FakerSingleton.GetInstance().Faker.Random.Guid();
    }

    private CreateUserRequest _user;
    private Guid _idempotencyKey;

    public override CreateUserCommand Build()
        => new(_idempotencyKey, _user);

    public CreateUserCommandBuilder WithUser(CreateUserRequest user)
    {
        _user = user;
        return this;
    }

    public CreateUserCommandBuilder WithIdempotencyKey(Guid idempotencyKey)
    {
        _idempotencyKey = idempotencyKey;
        return this;
    }
}
