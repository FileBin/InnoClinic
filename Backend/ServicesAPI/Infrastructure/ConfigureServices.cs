using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using InnoClinic.Shared.Domain.Abstractions;
using ServicesAPI.Infrastructure;
using ServicesAPI.Domain;
using ServicesAPI.Infrastructure.Repository;

namespace OfficesAPI.Infrastructure;

public static class ConfigureServices {
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config) {
        var dbSection = config.GetSection("ServicesDb");
        var connectionString = config.GetConnectionString("DefaultConnection");

        services.AddDbContext<ServicesDbContext>(options => options.UseSqlServer(connectionString));

        services.AddScoped<IRepository<Service>, ServiceRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
