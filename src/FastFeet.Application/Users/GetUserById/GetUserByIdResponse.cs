using System.Net;
using FastFeet.Application.Commons.Response;
using FastFeet.Domain.Entities;

namespace FastFeet.Application.Users.GetUserById;

public sealed class GetUserByIdResponse : ResponseBase
{
    public User? User { get; }

    public GetUserByIdResponse(HttpStatusCode httpStatusCode, User? user) : base(httpStatusCode)
        => User = user;

    public static GetUserByIdResponse Ok(User user)
        => new(HttpStatusCode.OK, user);

    public static GetUserByIdResponse NoContent()
        => new(HttpStatusCode.OK, null);
}
