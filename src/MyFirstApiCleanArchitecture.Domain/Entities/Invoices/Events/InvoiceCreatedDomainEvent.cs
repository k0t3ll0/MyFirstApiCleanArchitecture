using MyFirstApiCleanArchitecture.Domain.Abstraction;

namespace MyFirstApiCleanArchitecture.Domain.Entities.Invoices.Events;

public record InvoiceCreatedDomainEvent(Guid invoiceId) : IDomainEvent;


