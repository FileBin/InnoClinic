using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OfficesAPI.Presentation.Controllers;
using Shared.Misc;

namespace OfficesAPI.Presentation;

public static class ConfigureServices {
    public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration config) {
        services.AddControllers().AddApplicationPart(typeof(OfficeController).Assembly);

        services.AddAuthorization(options => {
            options.AddPolicy(Config.OfficePolicy, policy => {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("scope", config.GetOrThrow("IdentityServer:ApiScope:Name"));
                policy.RequireRole(config.GetOrThrow("AdminRoleName"));
            });
        });
        return services;
    }

    public static void UsePresentation(this WebApplication app) {
        app.UseRouting();
        app.MapControllers();
    }
}