using Filebin.Common.Util;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OfficesAPI.Domain.Models;
using OfficesAPI.Infrastructure.Database;
using OfficesAPI.Infrastructure.Repository;
using Shared.Domain.Abstractions;

namespace OfficesAPI.Infrastructure;

public static class ConfigureServices {
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config) {
        var section = config.GetSection("OfficesDb");
        var connectionString = 
            $"{section.GetOrThrow("Prefix")}" +
            $"{section.GetOrThrow("User")}:{section.GetOrThrow("Password")}@" + 
            $"{section.GetOrThrow("Host")}:{section.GetOrThrow("Port")}";

        services.AddMongoDB<OfficeDbContext>(connectionString, section.GetOrThrow("Database"));

        services.AddScoped<IRepository<Office>, OfficeRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
