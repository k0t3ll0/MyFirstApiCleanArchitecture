using MyFirstApiCleanArchitecture.Domain.Abstraction;
using MyFirstApiCleanArchitecture.Domain.Entities.Customers.DTOs;
using MyFirstApiCleanArchitecture.Domain.Entities.Customers.Events;
using MyFirstApiCleanArchitecture.Domain.Entities.Customers.ValueObjects;
using MyFirstApiCleanArchitecture.Domain.Entities.Invoices;
using MyFirstApiCleanArchitecture.Domain.Entities.Shared;

namespace MyFirstApiCleanArchitecture.Domain.Entities.Customers;

public sealed class Customer : BaseEntity
{
    private Customer() { }

    private Customer(Title title, Adress adress, Money balance)
    {
        Title = title;
        Adress = adress;
        Balance = balance;
    }

    public Title Title { get; private set; } = null!;

    public Adress Adress { get; private set; } = null!;

    public Money Balance { get; private set; } = null!;

    public ICollection<Invoice> Invoices { get; private set; } = null!;

    public static Customer CreateCustomer(
        CreateCustomerDto request)
    {
        var customer = new Customer(

            new Title(request.Title),
            new Adress(
                request.FirstLineAdress,
                request.SecondLineAdress,
                request.Postcode,
                request.City,
                request.Country),
            new Money(0));

        customer.RaiseDomainEvent(
            new CustomerCreatedDomainEvent(customer.Id));
        return customer;
    }

    public void Update(UpdateCustomerDto request)
    {
        Title = new Title(request.Title);
        Adress = new Adress(
                request.FirstLineAdress,
                request.SecondLineAdress,
                request.Postcode,
                request.City,
                request.Country);
    }
}
