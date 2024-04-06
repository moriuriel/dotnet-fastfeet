using FastFeet.Application.Commons.Command;
using FastFeet.Application.Commons.Response;
using FastFeet.Infrastructure.ExternalService.Idempotency;
using MediatR;

namespace FastFeet.Application.Commons.Behaviors;

internal sealed class IdempotentCommandPipelineBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IdempotentCommand
    where TResponse : ResponseBase
{
    private readonly IIdempotencyService _idempotencyService;

    public IdempotentCommandPipelineBehavior(IIdempotencyService idempotencyService)
        => _idempotencyService = idempotencyService;

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (await _idempotencyService.RequestExistsAsync(request.RequestId))
            return default!;

        var response = await next();

        await _idempotencyService.CreateRequestAsync(request.RequestId, typeof(TRequest).Name);

        return response;
    }
}
