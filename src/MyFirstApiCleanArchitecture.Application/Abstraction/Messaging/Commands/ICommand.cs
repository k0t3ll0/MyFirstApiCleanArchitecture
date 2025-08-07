using MediatR;
using MyFirstApiCleanArchitecture.Domain.Abstraction;

namespace MyFirstApiCleanArchitecture.Application.Abstraction.Messaging.Commands;

public interface ICommand : IRequest<Result<NoContentDto>>, IBaseCommand;//for Update or Remove commands

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand//for Create command
    where TResponse : IResult;

public interface IBaseCommand; //inherit interface to quick using both upper interfaces