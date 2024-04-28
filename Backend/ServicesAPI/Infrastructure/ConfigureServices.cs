using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using InnoClinic.Shared.Domain.Abstractions;
using ServicesAPI.Domain;
using ServicesAPI.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.AspNetCore.Builder;

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
        context.Database.EnsureCreated();
    }
}
