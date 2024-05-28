using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using InnoClinic.Shared.Misc;

namespace OfficesAPI.Presentation;

public static class ConfigureServices {
    public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration config) {
        services.AddEndpoints(typeof(ConfigureServices).Assembly);

        services.AddAuthorizationBuilder()
            .AddRoledPolicy(config, Config.ReceptionistPolicy, config.GetOrThrow("ReceptionistRoleName"))
            .AddRoledPolicy(config, Config.DoctorPolicy, config.GetOrThrow("DoctorRoleName"))
            .AddRoledPolicy(config, Config.PatientPolicy, config.GetOrThrow("PatientRoleName"));

        return services;
    }

    public static AuthorizationBuilder AddRoledPolicy(this AuthorizationBuilder authorizationBuilder,  IConfiguration config, string policyName, string roleName)
    => authorizationBuilder.AddPolicy(policyName, policy => {
        policy.RequireAuthenticatedUser();
        policy.RequireAssertion(context => {
            var adminRoleName = config.GetOrThrow("AdminRoleName");

            if (context.User.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == adminRoleName))
                return true;

            var scopeName = config.GetOrThrow("IdentityServer:ApiScope:Name");

            return context.User.HasClaim(x => x.Type == "scope" && x.Value == scopeName) &&
                   context.User.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == roleName);
        });
    });

    public static void UsePresentation(this WebApplication app) {
        app.UseRouting();
        app.MapEndpoints();
    }
}