namespace FastFeet.Domain.Entities;

public class Entity
{
    public Guid Id { get; protected init; }

    protected Entity(Guid id)
        => Id = id;
}