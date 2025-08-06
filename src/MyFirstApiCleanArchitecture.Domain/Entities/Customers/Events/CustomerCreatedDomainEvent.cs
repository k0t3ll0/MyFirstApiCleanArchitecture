using MyFirstApiCleanArchitecture.Domain.Abstraction;

namespace MyFirstApiCleanArchitecture.Domain.Entities.Customers.Events
{
    public record class CustomerCreatedDomainEvent(Guid customerId) : IDomainEvent;  
}
