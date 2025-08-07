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
        CreateCustomerDto Dto)
    {
        var customer = new Customer(

            new Title(Dto.Title),
            new Adress(
                Dto.FirstLineAdress,
                Dto.SecondLineAdress,
                Dto.Postcode,
                Dto.City,
                Dto.Country),
            new Money(0));

        customer.RaiseDomainEvent(
            new CustomerCreatedDomainEvent(customer.Id));
        return customer;
    }

    public void Update(UpdateCustomerDto Dto)
    {
        Title = new Title(Dto.Title);
        Adress = new Adress(
                Dto.FirstLineAdress,
                Dto.SecondLineAdress,
                Dto.Postcode,
                Dto.City,
                Dto.Country);
    }
}
