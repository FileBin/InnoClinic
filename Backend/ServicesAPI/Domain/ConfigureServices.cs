using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace ServicesAPI.Domain;

public static class ConfigureServices {
    public static IServiceCollection AddDomain(this IServiceCollection services) {
        services.AddValidatorsFromAssembly(typeof(ConfigureServices).Assembly, includeInternalTypes: true);
        return services;
    }
}
