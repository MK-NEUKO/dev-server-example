using System.Reflection;
using EnvironmentGateway.Api.GatewayConfiguration;
using EnvironmentGateway.Infrastructure;
using EnvironmentGateway.Application;
using EnvironmentGatewayApi.Extensions;
using EnvironmentGatewayApi.GatewayConfiguration;
using EnvironmentGatewayApi.GatewayConfiguration.Abstractions;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Services.AddSingleton<IRuntimeConfigurator, RuntimeConfigurator>();

var init = InitialConfigurator.GetInitialConfiguration();
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
}

app.MapEndpoints();

app.MapReverseProxy();

app.UseHttpsRedirection();

app.UseAuthorization();

await app.RunAsync();
