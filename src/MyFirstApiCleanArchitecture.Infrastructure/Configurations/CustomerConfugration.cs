using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyFirstApiCleanArchitecture.Domain.Entities.Customers;
using MyFirstApiCleanArchitecture.Domain.Entities.Shared;

namespace MyFirstApiCleanArchitecture.Infrastructure.Configurations;

public class CustomerConfugration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        //Связь для нашего типа Адрес, т.к. он ValueObject(объект-значение) и имеет много параметров
        builder.OwnsOne(customer => customer.Adress, adress => 
        {
            adress.Property(adress => adress.FirstLineAdress)
                .IsRequired()
                .HasMaxLength(40);

            adress.Property(adress => adress.SecondLineAdress)
                .HasMaxLength(40);

            adress.Property(adress => adress.Postcode)
                .IsRequired()
                .HasMaxLength(10);

            adress.Property(adress => adress.City)
                .IsRequired()
                .HasMaxLength(20);

            adress.Property(adress => adress.Country)
                .IsRequired()
                .HasMaxLength(20);
        });

        //Когда мы имеем VO(Объект-значение) с одним параметром.
        builder.Property(customer => customer.Title)
            .HasConversion(
                title => title.Value,
                value => new Title(value))
            .IsRequired()
            .HasMaxLength(45);

        builder.Property(Customer => Customer.Balance)
            .HasConversion(
                balance => balance.Value,
                value => new Money(value))
            .IsRequired()
            .HasPrecision(18, 2);//указываем точность нашего типа данных, т.к. он денежный(для бизнеса)

        builder.HasMany(customer => customer.Invoices) //связь много счетов-фактур
            .WithOne(x => x.Customer)//к одному клиенту
            .HasForeignKey(x => x.CustomerId)//внешний ключ - айди клиента
            .OnDelete(DeleteBehavior.Restrict);//если есть счёт для клиента, клиента не удалить

        builder.Property(x => x.RowVersion).IsRowVersion();
    }
}
