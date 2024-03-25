using System.Net;
using FastFeet.Application.Commons.Response;
using FastFeet.Domain.Entities;

namespace FastFeet.Application.Users.GetUserById;

public sealed class GetUserByIdResponse : ResponseBase
{
    public UserResponse? User { get; }

    public GetUserByIdResponse(HttpStatusCode httpStatusCode, UserResponse? user) : base(httpStatusCode)
        => User = user;

    public static GetUserByIdResponse Ok(User user)
        => new(HttpStatusCode.OK, new(user.Id, user.Name, user.Active, user.UserType, user.CreatedAt));

    public static GetUserByIdResponse NoContent()
        => new(HttpStatusCode.OK, null);
}