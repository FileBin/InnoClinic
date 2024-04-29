using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using InnoClinic.Shared.Domain.Abstractions;
using ServicesAPI.Domain;
using ServicesAPI.Infrastructure.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace ServicesAPI.Infrastructure;

public static class ConfigureServices {
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config) {
        var connectionString = config.GetConnectionString("DefaultConnection");

        services.AddDbContext<ServicesDbContext>(options => options.UseSqlServer(connectionString));

        services.AddScoped<IRepository<Service>, ServiceRepository>();
        services.AddScoped<IRepository<ServiceCategory>, ServiceCategoryRepository>();
        services.AddScoped<IRepository<Specialization>, SpecializationsRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

    public static void EnsureDatabaseCreated(this WebApplication app) {
        using var serviceScope = app.Services.GetService<IServiceScopeFactory>()?.CreateScope();
        ArgumentNullException.ThrowIfNull(serviceScope);

        var context = serviceScope.ServiceProvider.GetRequiredService<ServicesDbContext>();
        try {
            context.Database.EnsureCreated();
        } catch (SqlException e) {
            app.Logger.LogWarning(e, "Database creating exited with errors");
        }
    }
}
