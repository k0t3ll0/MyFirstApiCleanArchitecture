namespace MyFirstApiCleanArchitecture.Domain.Entities.InvoiceItems.DTOs;

public abstract class CreateInvoiceItemDto
{
    public Guid ProductId { get; set; }

    public int Quantity { get; set; }
}
