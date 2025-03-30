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

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Services.AddSingleton<IRuntimeConfigurator, RuntimeConfigurator>();
builder.Services.AddSingleton<IInitialConfigurator, InitialConfigurator>();
var initialConfigurator = builder.Services.BuildServiceProvider()!.GetService<IInitialConfigurator>();
var init = await initialConfigurator!.GetInitialConfigurationAsync();
builder.Services.AddReverseProxy()
    .LoadFromMemory(init.Routes, init.Clusters);

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

app.MapEndpoints();

app.UseCustomExceptionHandler();

app.MapReverseProxy();

app.UseHttpsRedirection();

await app.RunAsync();
