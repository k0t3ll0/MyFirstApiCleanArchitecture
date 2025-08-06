using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyFirstApiCleanArchitecture.Domain.Entities.Invoices;
using MyFirstApiCleanArchitecture.Domain.Entities.Invoices.ValueObjects;
using MyFirstApiCleanArchitecture.Domain.Entities.Shared;

namespace MyFirstApiCleanArchitecture.Infrastructure.Configurations;

public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.Property(invoice => invoice.PoNumber)
            .HasConversion(
                poNumber => poNumber.Value,
                value => new PoNumber(value))
            .IsRequired()
            .HasMaxLength(45);

        builder.Property(invoice => invoice.TotalBalance)
            .HasConversion(
                totalBalance => totalBalance.Value,
                value => new Money(value))
            .IsRequired()
            .HasPrecision(18, 2);

        builder.HasMany(invoice => invoice.PurchasedProducts)
            .WithOne(x=>x.Invoice)
            .HasForeignKey(x => x.InvoiceId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.RowVersion).IsRowVersion();
    }
}
