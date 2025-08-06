using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MyFirstApiCleanArchitecture.Infrastructure;

public static class ServiceRegister
{
    public static IServiceCollection AddInfrastructureServices(//добавляем сервисы из слоя инфраструктуры
        this IServiceCollection services,
        IConfiguration config)
    {
        AddDbConnection(services, config);//вызов подключения контекста бд

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
}
