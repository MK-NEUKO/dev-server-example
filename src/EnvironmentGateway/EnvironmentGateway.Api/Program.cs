using System.Reflection;
using EnvironmentGateway.Api.Extensions;
using EnvironmentGateway.Api.GatewayConfiguration;
using EnvironmentGateway.Infrastructure;
using EnvironmentGateway.Application;
using EnvironmentGatewayApi.Extensions;
using EnvironmentGatewayApi.GatewayConfiguration;
using EnvironmentGatewayApi.GatewayConfiguration.Abstractions;
using MediatR;
using Scalar.AspNetCore;
using Serilog;
using Yarp.ReverseProxy.Configuration;

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
    // app.ApplyMigrations();
}

using (var scope = app.Services.CreateScope())
{
    var runtimeConfigurator = scope.ServiceProvider.GetRequiredService<IRuntimeConfigurator>();
    await runtimeConfigurator.InitializeGateway();
}

app.UseHttpsRedirection();

app.UseSerilogRequestLogging();

app.MapEndpoints();

app.UseCustomExceptionHandler();

app.MapReverseProxy();

await app.RunAsync();

