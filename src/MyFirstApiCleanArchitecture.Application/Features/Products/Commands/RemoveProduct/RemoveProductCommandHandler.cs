using MyFirstApiCleanArchitecture.Application.Abstraction.Messaging.Commands;
using MyFirstApiCleanArchitecture.Domain.Abstraction;
using MyFirstApiCleanArchitecture.Domain.Entities.Products;

namespace MyFirstApiCleanArchitecture.Application.Features.Products.Commands.RemoveProduct;

internal sealed class RemoveProductCommandHandler(
    IUnitOfWork unitOfWork) : ICommandHandler<RemoveProductCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<NoContentDto>> Handle(
        RemoveProductCommand request, 
        CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.Repository<Product>()
            .GetByIdAsync(request.ProductId, cancellationToken);

        if (product is null)
            return Result<NoContentDto>
                .Failed(400, "Null.Error", $"The product with the id: {request.ProductId} not exist");

        _unitOfWork.Repository<Product>()
            .Delete(product);

        await _unitOfWork.CommitAsync(cancellationToken);

        return Result<NoContentDto>
            .Success(204);
    }
}
