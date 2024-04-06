namespace FastFeet.Infrastructure.ExternalService.Idempotency;
public interface IIdempotencyService
{
    Task<bool> RequestExistsAsync(Guid requestId);
    Task CreateRequestAsync(Guid reuqestId, string name);
}


