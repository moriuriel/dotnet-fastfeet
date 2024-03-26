using FastFeet.Application.Users.GetUserById;
using FastFeet.Test.Unit.Commons;

namespace FastFeet.Test.Unit.Application.Users.GetUserByIdTest;

public class GetUserByIdQueryBuilder : BuilderBase<GetUserByIdQuery>
{
    public GetUserByIdQueryBuilder()
        => _userId = FakerSingleton.GetInstance().Faker.Random.Guid();

    private Guid _userId;

    public GetUserByIdQueryBuilder WithEmptyUserId()
    {
        _userId = Guid.Empty;
        return this;
    }

    public override GetUserByIdQuery Build()
        => new(_userId);
}
