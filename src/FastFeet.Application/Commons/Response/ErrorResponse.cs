using System.Net;

namespace FastFeet.Application.Commons.Response;

public class ErrorResponse : ResponseBase
{
    private ErrorResponse(HttpStatusCode httpStatusCode, List<string> errors) : base(httpStatusCode)
        => Errors = errors;

    public List<string> Errors { get; private set; }

    public static ErrorResponse UnprocessableEntity(List<string> errors)
        => new(httpStatusCode: HttpStatusCode.UnprocessableEntity, errors);

    public static ErrorResponse UnprocessableEntity(string error)
        => new(HttpStatusCode.UnprocessableEntity,
               errors: new() { error });

    public static ErrorResponse Conflict(string error)
        => new(HttpStatusCode.Conflict,
               errors: new() { error });
}