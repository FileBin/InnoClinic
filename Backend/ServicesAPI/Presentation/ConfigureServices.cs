using InnoClinic.Shared.Misc;
using InnoClinic.Shared.Misc.Auth;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServicesAPI.Presentation.Controllers;

namespace ServicesAPI.Presentation;

public static class ConfigureServices {
    public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration config) {
        services.AddControllers().AddApplicationPart(typeof(ServicesController).Assembly);
        services.AddTransient<ClaimUserDescriptorFactory>();

        services.AddAuthorizationBuilder()
            .AddPolicy(Config.ServicesPolicy, policy => {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("scope", config.GetOrThrow("IdentityServer:ApiScope:Name"));
                policy.RequireRole(config.GetOrThrow("AdminRoleName"));
            });

        return services;
    }

    public static void UsePresentation(this WebApplication app) {
        app.UseRouting();
        app.MapControllers();
    }
}
