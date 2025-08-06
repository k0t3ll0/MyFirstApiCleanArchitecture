using MyFirstApiCleanArchitecture.Domain.Abstraction;
using MyFirstApiCleanArchitecture.Domain.Entities.Customers;
using MyFirstApiCleanArchitecture.Domain.Entities.InvoiceItems;
using MyFirstApiCleanArchitecture.Domain.Entities.InvoiceItems.ValueObjects;
using MyFirstApiCleanArchitecture.Domain.Entities.Invoices.DTOs;
using MyFirstApiCleanArchitecture.Domain.Entities.Invoices.Events;
using MyFirstApiCleanArchitecture.Domain.Entities.Invoices.ValueObjects;
using MyFirstApiCleanArchitecture.Domain.Entities.Products;
using MyFirstApiCleanArchitecture.Domain.Entities.Shared;

namespace MyFirstApiCleanArchitecture.Domain.Entities.Invoices;

public sealed class Invoice : BaseEntity
{
    private Invoice() { }

    private Invoice(
        Guid invoceId,
        PoNumber poNumber,
        Guid customerId,
        ICollection<InvoiceItem> purchasedProducts,
        Money totalBalance) : base(invoceId)
    {
        PoNumber = poNumber;
        CustomerId = customerId;
        PurchasedProducts = purchasedProducts;
        TotalBalance = totalBalance;
    }

    public PoNumber PoNumber { get; private set; } = null!;

    public Guid CustomerId { get; private set; }

    public Customer Customer { get; private set; } = null!;

    public ICollection<InvoiceItem> PurchasedProducts { get; private set; } = null!;

    public Money TotalBalance { get; private set; } = null!;

    public static async Task<Invoice> Create(
        CreateInvoiceDto request,
        IUnitOfWork unitOfWork)
    {
        //проверка что количество продуктов не пустое
        if (request.PurchasedProducts is null || request.PurchasedProducts.Count == 0)
            throw new InvalidOperationException("Empty Invoice can not be created");

        var invoiceId = Guid.NewGuid(); //создаём новый Id для счёта
        ICollection<InvoiceItem> purchaseProducts = []; //создаём коллекцию купленных товаров

        foreach (var purchasedProduct in request.PurchasedProducts) //в цикле получаем продукт через unitOfWork(нужен для запросов к БД)
        {
            var product = await unitOfWork
                .Repository<Product>()//метод репозитория для получения доступа к универсальным командам
                .GetByIdAsync(purchasedProduct.ProductId) ?? //проверка на null
                throw new ArgumentNullException($"Product with id: {purchasedProduct.ProductId} not found");

            var invoiceItem = new InvoiceItem(//создаём новый предмет счёта
                Guid.NewGuid(),
                new Money(product.UnitPrice.Value),
                new Quantity(purchasedProduct.Quantity),
                invoiceId);

            purchaseProducts.Add(invoiceItem);
        }

        var totalBalance = purchaseProducts //рассчитываем итоговый баланс
            .Sum(x => x.TotalPrice.Value);

        var invoice = new Invoice(//создаём счёт
            invoiceId,
            new PoNumber(request.PoNumber),
            request.CustomerId,
            purchaseProducts,
            new Money(totalBalance));

        invoice.RaiseDomainEvent(//вызов события "уведомить о создании"
            new InvoiceCreatedDomainEvent(invoiceId));
        
        return invoice;//вернуть счёт
    } 

    public void Update(UpdateInvoiceDto request)
    {
        PoNumber = new PoNumber(request.PoNumber);
    }
}
