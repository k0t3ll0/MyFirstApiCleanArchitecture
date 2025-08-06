namespace MyFirstApiCleanArchitecture.Domain.Abstraction;

public abstract class BaseEntity
{
    private readonly List<IDomainEvent> _domainEvents = [];

    public BaseEntity() { }

    public BaseEntity(Guid id) => Id = id;
    

    public Guid Id { get; init; }

    public byte[] RowVersion { get; set; } = null!; //для проверки изменений

    public IReadOnlyList<IDomainEvent> GetDomainEvents() 
        => _domainEvents.ToList();

    public void ClearDomainEvents() 
        => _domainEvents.Clear();

    protected void RaiseDomainEvent(IDomainEvent domainEvent)
        =>_domainEvents.Add(domainEvent);
}
