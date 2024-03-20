using FastFeet.Application.Commons.Command;
using FastFeet.Application.Commons.Response;
using MediatR;

namespace FastFeet.Application.Commons.Behaviors;

public sealed class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : CommandBase
    where TResponse : ResponseBase
{

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if(!request.IsValid())
            return (ErrorResponse.UnprocessableEntity(request.Errors) as TResponse)!;

        return await next();
    }
}