using System.Reflection;
using EnvironmentGateway.Api.Extensions;
using EnvironmentGateway.Api.GatewayConfiguration;
using EnvironmentGateway.Api.GatewayConfiguration.Abstractions;
using EnvironmentGateway.Application;
using EnvironmentGateway.Infrastructure;
using Scalar.AspNetCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.AddServiceDefaults();

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);



builder.Services.AddScoped<IRuntimeConfigurator, RuntimeConfigurator>();
builder.Services.AddScoped<IInitialConfigurator, InitialConfigurator>();

builder.Services.AddReverseProxy()
    .LoadFromMemory(PreConfiguration.GetRoutes(), PreConfiguration.GetClusters());


builder.Services.AddOpenApi();

builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());
    
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();

    // Apply migrations in development mode without using aspire.
#pragma warning disable S125
    app.ApplyMigrations();
#pragma warning restore S125
}

using (var scope = app.Services.CreateScope())
{
    var runtimeConfigurator = scope.ServiceProvider.GetRequiredService<IRuntimeConfigurator>();
    await runtimeConfigurator.InitializeGateway();
}

app.UseHttpsRedirection();

app.UseRequestContextLogging();

app.UseSerilogRequestLogging();

app.MapEndpoints();

app.UseCustomExceptionHandler();

app.MapReverseProxy();

await app.RunAsync();

namespace EnvironmentGateway.Api
{
    public partial class Program;
}