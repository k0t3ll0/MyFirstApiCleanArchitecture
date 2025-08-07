using AutoMapper;
using MyFirstApiCleanArchitecture.Application.Abstraction.Messaging.Commands;
using MyFirstApiCleanArchitecture.Domain.Abstraction;
using MyFirstApiCleanArchitecture.Domain.Entities.Products;

namespace MyFirstApiCleanArchitecture.Application.Features.Products.Commands.CreateProduct;

//Обработчику даётся интерфейс обработчика<команда, ответ>
internal sealed class CreateProductCommandHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper) : ICommandHandler<CreateProductCommand, ProductResponse>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;//DI for UnitOfWork
    private readonly IMapper _mapper = mapper;

    public async Task<Result<ProductResponse>> Handle(
        CreateProductCommand request, 
        CancellationToken cancellationToken)
    {
        var product = Product.Create(request.Dto);//Create a new product from Dto

        //using unitOfWork and his repository<generic type> method we get our entity and use create async
        await _unitOfWork.Repository<Product>() 
            .CreateAsync(product, cancellationToken);

        //save changes with token
        await _unitOfWork.CommitAsync(cancellationToken);

        //create a new response with automapper
        var response = _mapper.Map<ProductResponse>(product);

        //return our result with success state and 201(created) status code
        return Result<ProductResponse>.Success(response, 201);
    }
}

