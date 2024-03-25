namespace FastFeet.Domain.Entities;

public class AggregateRoot : Entity
{
    public AggregateRoot(Guid id) : base(id) { }

    protected AggregateRoot()
    {
    }
}
