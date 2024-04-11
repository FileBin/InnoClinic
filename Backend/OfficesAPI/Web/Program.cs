using Shared.Misc;
using OfficesAPI.Infrastructure;
using OfficesAPI.Application;
using OfficesAPI.Presentation;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Logging;


var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddUtils()
    .AddInfrastructure(builder.Configuration)
    .AddApplication()
    .AddIdentityServer(builder.Configuration, builder.Environment)
    .AddPresentation(builder.Configuration);

var app = builder.Build();

app.UseUtils();

if (!app.Environment.IsProduction()) {
    app.MapGet("/userinfo", [Authorize] (ClaimsPrincipal user) => user.Claims.Select(x => new { x.ValueType, x.Type, x.Value }));
}
app.MapControllers();

app.UseIdentityServer();
app.Run();
