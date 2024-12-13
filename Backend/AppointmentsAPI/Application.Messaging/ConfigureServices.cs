using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AppointmentsAPI.Application.Messaging;

public static class ConfigureServices {
    public static IServiceCollection AddMessaging(this IServiceCollection services, IConfiguration config) {
        var assembly = typeof(ConfigureServices).Assembly;

        services.AddMassTransit(c => {
            c.SetKebabCaseEndpointNameFormatter();
            c.AddConsumers(assembly);
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