using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyFirstApiCleanArchitecture.Domain.Entities.Products;
using MyFirstApiCleanArchitecture.Domain.Entities.Shared;

namespace MyFirstApiCleanArchitecture.Infrastructure.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(product => product.UnitPrice)
            .HasConversion(
                unitPrice => unitPrice.Value,
                value => new Money(value))
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(product => product.Description)
            .HasConversion(
                desc => desc.Value,
                value => new Title(value))
            .IsRequired()
            .HasMaxLength(45);

        builder.Property(x => x.RowVersion).IsRowVersion();
    }
}
