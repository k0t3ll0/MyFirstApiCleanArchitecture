using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyFirstApiCleanArchitecture.Domain.Entities.InvoiceItems;
using MyFirstApiCleanArchitecture.Domain.Entities.InvoiceItems.ValueObjects;
using MyFirstApiCleanArchitecture.Domain.Entities.Shared;

namespace MyFirstApiCleanArchitecture.Infrastructure.Configurations;

public class InvoiceItemsConfigturation : IEntityTypeConfiguration<InvoiceItem>
{
    public void Configure(EntityTypeBuilder<InvoiceItem> builder)
    {
        builder.Property(item => item.SellPrice)
            .HasConversion(
                sellPrice => sellPrice.Value,
                value => new Money(value))
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(item => item.TotalPrice)
            .HasConversion(
                totalPrice => totalPrice.Value,
                value => new Money(value))
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(item => item.Quantity)
            .HasConversion(
                quantity => quantity.Value,
                value => new Quantity(value))
            .IsRequired();


        builder.Property(x => x.RowVersion).IsRowVersion();
    }
}
