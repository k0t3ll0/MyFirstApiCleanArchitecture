using MyFirstApiCleanArchitecture.Application.Abstraction.Messaging.Commands;
using MyFirstApiCleanArchitecture.Domain.Entities.Products.Dtos;

namespace MyFirstApiCleanArchitecture.Application.Features.Products.Commands.CreateProduct;

public record CreateProductCommand(
    CreateProductDto Dto) : ICommand<ProductResponse>;

