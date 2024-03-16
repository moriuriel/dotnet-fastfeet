using MediatR;

namespace FastFeet.Application.Commons.Behaviors;
internal sealed class RequestLoggingPipelineBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
    where TResponse : Response
{

}
