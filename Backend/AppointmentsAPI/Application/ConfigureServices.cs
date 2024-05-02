using System.Runtime.CompilerServices;
using AppointmentsAPI.Application.Behaviours;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("AppointmentsAPI.Tests", AllInternalsVisible = true)]

namespace AppointmentsAPI.Application;

public static class ConfigureServices {
    public static IServiceCollection AddApplication(this IServiceCollection services) {
        var assembly = typeof(ConfigureServices).Assembly;
        
        services.AddValidatorsFromAssembly(assembly, includeInternalTypes: true);

        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(assembly);
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        });
        
        return services;
    }
}