using Microsoft.Extensions.DependencyInjection;
using OfficesAPI.Application.Contracts.Services;
using OfficesAPI.Application.Services;

namespace OfficesAPI.Application;

public static class ConfigureServices {
    public static IServiceCollection AddApplication(this IServiceCollection services) {
        services.AddScoped<IOfficeService, OfficeService>();
        return services;
    }
}