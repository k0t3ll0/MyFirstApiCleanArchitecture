using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyFirstApiCleanArchitecture.Domain.Abstraction;
using MyFirstApiCleanArchitecture.Infrastructure.Repositories;
using MyFirstApiCleanArchitecture.Infrastructure.UnitOfWorks;

namespace MyFirstApiCleanArchitecture.Infrastructure;

public static class ServiceRegister
{
    public static IServiceCollection AddInfrastructureServices(//добавляем сервисы из слоя инфраструктуры
        this IServiceCollection services,
        IConfiguration config)
    {
        AddDbConnection(services, config);//вызов подключения контекста бд
        AddServicesToDiContainer(services);
        return services;//возвращаем коллекцию сервисов
    }
    
    //инкапсулируем(скрываем) добавление контекста бд в отдельном методе расширения
    private static IServiceCollection AddDbConnection(this IServiceCollection services,
        IConfiguration config)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(config.GetConnectionString("Database"));
        });
        return services;
    }

    //Добавляем в DI контейнер наши сервисы(UnitOfWork и Repository)
    private static IServiceCollection AddServicesToDiContainer(this IServiceCollection services)
    {
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
}
