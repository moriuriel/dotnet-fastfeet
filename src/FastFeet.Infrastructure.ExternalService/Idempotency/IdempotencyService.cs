namespace FastFeet.Infrastructure.ExternalService.Idempotency;

public class IdempotencyService : IIdempotencyService
{
    public IdempotencyService()
    {
    }

    public Task CreateRequestAsync(Guid reuqestId, string name)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RequestExistsAsync(Guid requestId)
    {
        throw new NotImplementedException();
    }
}
