using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using ServicesAPI.Application.Contracts.Services;

[assembly: InternalsVisibleTo("ServicesAPI.Tests", AllInternalsVisible = true)]

namespace ServicesAPI.Application;

public static class ConfigureServices {
    public static IServiceCollection AddApplication(this IServiceCollection services) {
        services.AddScoped<IServicesService, ServicesService>();
        services.AddScoped<ISpecializationsService, SpecializationsService>();
        services.AddScoped<IServiceCategoriesService, ServiceCategoriesService>();
        return services;
    }
}