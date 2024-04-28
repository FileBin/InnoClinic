using Shared.Misc;
using ServicesAPI.Domain;
using ServicesAPI.Application;
using ServicesAPI.Infrastructure;
using ServicesAPI.Presentation;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;


var builder = WebApplication.CreateBuilder(args);

builder.AddLogger();

builder.Services
    .AddUtils()
    .AddIdentityServer(builder.Configuration, builder.Environment)
    .AddDomain()
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddPresentation(builder.Configuration);

var app = builder.Build();

app.UseUtils();

if (!app.Environment.IsProduction()) {
    app.MapGet("/userinfo", [Authorize] (ClaimsPrincipal user) => user.Claims.Select(x => new { x.ValueType, x.Type, x.Value }));
}

app.MapControllers();

app.UseIdentityServer();

app.EnsureDatabaseCreated();

app.Run();
