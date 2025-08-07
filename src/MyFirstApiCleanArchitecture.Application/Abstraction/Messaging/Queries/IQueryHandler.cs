using MediatR;
using MyFirstApiCleanArchitecture.Domain.Abstraction;

namespace MyFirstApiCleanArchitecture.Application.Abstraction.Messaging.Queries;

//Интерфейс обработчика запроса<запрос, ответ> наследуем от mediatr IRequest<запрос, ответ(в качестве ответа наш результат<ответ>)>
public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>//дженерик запрос от интерфейса запроса
    where TResponse : IResult; //ответ наш интерфейс результата

