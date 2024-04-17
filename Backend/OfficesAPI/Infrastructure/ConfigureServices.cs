using System.Web;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OfficesAPI.Domain.Models;
using OfficesAPI.Infrastructure.Database;
using OfficesAPI.Infrastructure.Repository;
using InnoClinic.Shared.Domain.Abstractions;
using Shared.Misc;

namespace OfficesAPI.Infrastructure;

public static class ConfigureServices {
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config) {
        var dbSection = config.GetSection("OfficesDb");
        var connectionString = 
            $"{dbSection.GetOrThrow("Prefix")}" +
            $"{HttpUtility.UrlEncode(dbSection.GetOrThrow("User"))}:{HttpUtility.UrlEncode(dbSection.GetOrThrow("Password"))}@" + 
            $"{dbSection.GetOrThrow("Host")}:{dbSection.GetOrThrow("Port")}";

        services.AddMongoDB<OfficeDbContext>(connectionString, dbSection.GetOrThrow("Database"));

        services.AddScoped<IRepository<Office>, OfficeRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
