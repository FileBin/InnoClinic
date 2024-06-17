using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using InnoClinic.Shared.Domain.Abstractions;
using InnoClinic.Shared.Misc;
using Npgsql;
using AppointmentsAPI.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using AppointmentsAPI.Infrastructure.Repository;
using AppointmentsAPI.Domain.Models;

namespace AppointmentsAPI.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        var dbSection = config.GetSection("AppointmentsDb");

        var dataSourceBuilder = new NpgsqlDataSourceBuilder($"Host={dbSection.GetOrThrow("Host")};"
                                    + $"Port={dbSection.GetOrThrow("Port")};"
                                    + $"Username={dbSection.GetOrThrow("User")};"
                                    + $"Password={dbSection.GetOrThrow("Password")};"
                                    + $"Database={dbSection.GetOrThrow("Database")};");

        var dataSource = dataSourceBuilder.Build();

        services.AddDbContext<AppointmentsDbContext>(options =>
            options.UseNpgsql(dataSource));

        services.AddScoped<IRepository<Appointment>, AppointmentsRepository>();
        services.AddScoped<IRepository<AppointmentResult>, AppointmentResultsRepository>();
        services.AddScoped<IRepository<Doctor>, DoctorsRepository>();
        services.AddScoped<IRepository<Patient>, PatientsRepository>();
        services.AddScoped<IRepository<Service>, ServicesRepository>();
        services.AddScoped<IRepository<Specialization>, SpecializationsRepository>();
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
