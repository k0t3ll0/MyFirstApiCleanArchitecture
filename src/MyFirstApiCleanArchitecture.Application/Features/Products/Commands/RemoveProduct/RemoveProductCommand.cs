using MyFirstApiCleanArchitecture.Application.Abstraction.Messaging.Commands;

namespace MyFirstApiCleanArchitecture.Application.Features.Products.Commands.RemoveProduct;

public record RemoveProductCommand(Guid ProductId) : ICommand;

