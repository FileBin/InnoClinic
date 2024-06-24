using Microsoft.Extensions.DependencyInjection;
using InnoClinic.Shared.Domain.Abstractions;
using InnoClinic.Shared.Misc;
using AppointmentsAPI.Infrastructure.Database;
using AppointmentsAPI.Infrastructure.Repository;
using Microsoft.AspNetCore.Builder;

namespace AppointmentsAPI.Infrastructure;

public static class ConfigureServices {
    public static IServiceCollection AddInfrastructure(this IServiceCollection services) {

        services.AddDbContext<AppointmentsDbContext>();
        services.AddRepositoriesFromAssembly(typeof(AppointmentsDbContext).Assembly);

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

    public static void EnsureDatabaseCreated(this WebApplication app, bool migrate = false) { 
        app.EnsureDatabaseCreated<AppointmentsDbContext>(migrate);
    }
}
