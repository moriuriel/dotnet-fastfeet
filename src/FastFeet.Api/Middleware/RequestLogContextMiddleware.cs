using Serilog.Context;

namespace FastFeet.Api.Middleware;

public class RequestLogContextMiddleware
{
    private readonly RequestDelegate _next;

    public RequestLogContextMiddleware(RequestDelegate next) => _next = next;

    public Task InvokeAsync(HttpContext httpContext)
    {
        using (LogContext.PushProperty("CorrelationId", httpContext.TraceIdentifier))
        {
            return _next(context: httpContext);
        };
    }
}
