using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OfficesAPI.Presentation.Controllers;
using InnoClinic.Shared.Misc;
using System.Security.Claims;

namespace OfficesAPI.Presentation;

public static class ConfigureServices {
    public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration config) {
        services.AddControllers().AddApplicationPart(typeof(OfficeController).Assembly);

        services.AddAuthorizationBuilder()
            .AddPolicy(Config.OfficePolicy, policy => {
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