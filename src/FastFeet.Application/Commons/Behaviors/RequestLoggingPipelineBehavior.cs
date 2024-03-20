using FastFeet.Application.Commons.Response;
using MediatR;
using Microsoft.Extensions.Logging;
using Serilog.Context;

namespace FastFeet.Application.Commons.Behaviors;

internal sealed class RequestLoggingPipelineBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
    where TResponse : ResponseBase
{
    private readonly ILogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> _logger;

    public RequestLoggingPipelineBehavior(ILogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> logger)
        => _logger = logger;

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        string requestName = typeof(TRequest).Name;

        _logger.LogInformation("Processing request {RequestName}", requestName);

        TResponse response = await next();

        if (response.IsSuccessStatusCode())
        {
            _logger.LogInformation("Completed request {RequestName}", requestName);
        }
        else
        {
            using (LogContext.PushProperty("Error", (response as ErrorResponse)!.Errors, true))
            {
                _logger.LogError("Completed Request {RequestName} with Error", requestName);
            }
        }

        return response;
    }
}
