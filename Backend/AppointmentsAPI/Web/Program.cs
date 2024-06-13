using InnoClinic.Shared.Misc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using AppointmentsAPI.Infrastructure;
using AppointmentsAPI.Application;
using OfficesAPI.Presentation;


var builder = WebApplication.CreateBuilder(args);

builder.AddLogger();

builder.Services
    .AddUtils()
    .AddIdentityServer(builder.Configuration, builder.Environment)
    .AddInfrastructure(builder.Configuration)
    .AddApplication()
    .AddPresentation(builder.Configuration);

var app = builder.Build();

app.UseUtils();

if (! app.Environment.IsProduction()) {
    app.MapGet("/userinfo", [Authorize] (ClaimsPrincipal user) => user.Claims.Select(x => new { x.ValueType, x.Type, x.Value }));
}

app.UsePresentation();
app.UseIdentityServer();
app.Run();
