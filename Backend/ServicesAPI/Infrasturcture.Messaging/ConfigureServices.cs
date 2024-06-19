﻿using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Messaging;


public static class ConfigureServices {
    public static IServiceCollection AddMessaging(this IServiceCollection services, IConfiguration config) {
        services.AddMassTransit(c => {
            c.UsingRabbitMq((context, cfg) => {
                cfg.Host(config["RabbitMq:Host"] ?? "localhost", c => {
                    c.Username(config["RabbitMq:Username"] ?? "guest");
                    c.Password(config["RabbitMq:Password"] ?? "guest");
                });
                cfg.ConfigureEndpoints(context);
            });
        });
        return services;
    }
}