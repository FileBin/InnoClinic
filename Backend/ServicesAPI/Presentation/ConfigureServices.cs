using System.Security.Claims;
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

                policy.RequireAssertion(context => {
                    var adminRoleName = config.GetOrThrow("AdminRoleName");

                    if (context.User.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == adminRoleName))
                        return true;

                    var scopeName = config.GetOrThrow("IdentityServer:ApiScope:Name");
                    var receptionistRoleName = config.GetOrThrow("ReceptionistRoleName");

                    return context.User.HasClaim(x => x.Type == "scope" && x.Value == scopeName) &&
                           context.User.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == receptionistRoleName);
                });
            });

        return services;
    }

    public static void UsePresentation(this WebApplication app) {
        app.UseRouting();
        app.MapControllers();
    }
}
