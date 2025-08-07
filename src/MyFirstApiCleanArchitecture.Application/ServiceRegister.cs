﻿using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MyFirstApiCleanArchitecture.Application;

public static class ServiceRegister 
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services)
    {
        services.AddAutoMapper(config => config.AddMaps(Assembly.GetExecutingAssembly()));

        return services;
    }
}
