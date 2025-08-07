using MediatR;
using MyFirstApiCleanArchitecture.Domain.Abstraction;

namespace MyFirstApiCleanArchitecture.Application.Abstraction.Messaging.Queries;

//интерфейс запроса<ответ> - наследуем от интерфейса запроса<ответ>
public interface IQuery<TResponse> : IRequest<Result<TResponse>>
    where TResponse : IResult;//где ответ является нашим интерфейсом результата

