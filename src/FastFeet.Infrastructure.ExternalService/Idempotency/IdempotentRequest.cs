﻿namespace FastFeet.Infrastructure.ExternalService.Idempotency;

internal sealed class IdempotentRequest
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public DateTime CreatedOnUtc { get; set; }
}