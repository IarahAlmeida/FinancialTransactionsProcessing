﻿using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(ApplicationServiceRegistration).Assembly);
        });

        return services;
    }
}