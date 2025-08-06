using Microsoft.EntityFrameworkCore;
using MyFirstApiCleanArchitecture.Domain.Entities.Customers;
using MyFirstApiCleanArchitecture.Domain.Entities.InvoiceItems;
using MyFirstApiCleanArchitecture.Domain.Entities.Invoices;
using MyFirstApiCleanArchitecture.Domain.Entities.Products;

namespace MyFirstApiCleanArchitecture.Infrastructure;

public class AppDbContext : DbContext
{
    
    public AppDbContext(DbContextOptions options) : base(options) { }

    protected AppDbContext() { }

    DbSet<Customer> Customers { get; set; }

    DbSet<Product> Products { get; set; }

    DbSet<Invoice> Invoices { get; set; }

    DbSet<InvoiceItem> InvoiceItems { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

}
