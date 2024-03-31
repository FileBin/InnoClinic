using Shared.Misc;
using OfficesAPI.Infrastructure;
using OfficesAPI.Application;
using OfficesAPI.Presentation;


var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddUtils()
    .AddInfrastructure(builder.Configuration)
    .AddApplication()
    .AddPresentation();

var app = builder.Build();

app.UseUtils();

app.MapControllers();

app.Run();
