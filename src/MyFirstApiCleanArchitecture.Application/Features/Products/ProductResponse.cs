using AutoMapper;
using MyFirstApiCleanArchitecture.Domain.Abstraction;
using MyFirstApiCleanArchitecture.Domain.Entities.Products;

namespace MyFirstApiCleanArchitecture.Application.Features.Products;

public class ProductResponse : IResult
{
    public Guid Id { get; set; }

    public string Description { get; set; } = null!;

    public decimal UnitPrice { get; set; }
}

public class ProductMapper : Profile
{
    public ProductMapper()
    {
        CreateMap<Product, ProductResponse>()
            //ForMember - using for map our ValueObjects to the fields types
            //MapFrom - from where need to map
            .ForMember(dto=>dto.Description, options=>options.MapFrom(entity=>entity.Description.Value))
            .ForMember(dto=>dto.UnitPrice, options=>options.MapFrom(entity=>entity.UnitPrice.Value));
    }
}