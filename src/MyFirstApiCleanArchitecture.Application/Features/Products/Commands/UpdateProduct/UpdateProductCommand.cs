using MyFirstApiCleanArchitecture.Application.Abstraction.Messaging.Commands;
using MyFirstApiCleanArchitecture.Domain.Entities.Products.Dtos;

namespace MyFirstApiCleanArchitecture.Application.Features.Products.Commands.UpdateProduct;

public record UpdateProductCommand(
    UpdateProductDto Dto) : ICommand;

