using FastFeet.Application.Commons.Response;
using FastFeet.Domain.Interfaces.Repository;
using MediatR;

namespace FastFeet.Application.Users.GetUserById;

public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, ResponseBase>
{
    public IUserRepository _userRepository;

    public GetUserByIdHandler(IUserRepository userRepository)
        => _userRepository = userRepository;

    public async Task<ResponseBase> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        if (!query.IsValid())
            return ErrorResponse.UnprocessableEntity(query.Errors);

        var user = await _userRepository.FindByIdAsync(query.UserId, cancellationToken);

        if (user is null)
            return GetUserByIdResponse.NoContent();

        return GetUserByIdResponse.Ok(user);
    }
}
