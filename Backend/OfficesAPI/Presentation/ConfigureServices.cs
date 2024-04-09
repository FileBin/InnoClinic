using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using OfficesAPI.Presentation.Controllers;

namespace OfficesAPI.Presentation;

public static class ConfigureServices {
    public static IServiceCollection AddPresentation(this IServiceCollection services) {
        services.AddControllers().AddApplicationPart(typeof(OfficeController).Assembly);
        return services;
    }

    public static void UsePresentation(this WebApplication app) {
        app.UseRouting();
        app.MapControllers();
    }
}