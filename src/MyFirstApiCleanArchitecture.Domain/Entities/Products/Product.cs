﻿using MyFirstApiCleanArchitecture.Domain.Abstraction;
using MyFirstApiCleanArchitecture.Domain.Entities.Products.Dtos;
using MyFirstApiCleanArchitecture.Domain.Entities.Shared;

namespace MyFirstApiCleanArchitecture.Domain.Entities.Products
{
    public sealed class Product :BaseEntity
    {
        private Product() { }

        private Product(
            Guid id,
            Title description,
            Money unitPrice) : base(id)
        {
            Description = description;
            UnitPrice = unitPrice;
        }

        public Title Description { get; private set; } = null!;

        public Money UnitPrice { get; private set; } = null!;

        public static Product Create(CreateProductDto request)
            => new (
                Guid.NewGuid(), 
                new Title(request.Description), 
                new Money(request.UnitPrice));

        public void Update(UpdateProductDto request)
        {
            Description = new Title(request.Description);
            UnitPrice = new Money(request.UnitPrice);
        }
    }
}
