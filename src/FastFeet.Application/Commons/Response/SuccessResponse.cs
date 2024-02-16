using System.Net;

namespace FastFeet.Application.Commons.Response;

public sealed class SuccessResponse : Response
{
    private SuccessResponse(HttpStatusCode statusCode) : base(statusCode) { }

    private SuccessResponse(HttpStatusCode statusCode, Guid id) : base(statusCode)
        => Id = id;

    public Guid? Id { get; private set; }

    public static SuccessResponse Created(Guid id)
        => new(HttpStatusCode.Created, id);

    public static SuccessResponse NoContent()
        => new(HttpStatusCode.NoContent);
}