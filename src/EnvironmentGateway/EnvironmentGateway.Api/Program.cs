using System.Reflection;
using EnvironmentGateway.Api.Extensions;
using EnvironmentGateway.Api.GatewayConfiguration;
using EnvironmentGateway.Api.GatewayConfiguration.Abstractions;
using EnvironmentGateway.Application;
using EnvironmentGateway.Infrastructure;
using Scalar.AspNetCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.AddServiceDefaults();

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);



builder.Services.AddScoped<IRuntimeConfigurator, RuntimeConfigurator>();
builder.Services.AddScoped<ICurrentConfigProvider, CurrentConfigProvider>();

builder.Services.AddReverseProxy()
    .LoadFromMemory(DefaultProxyConfigProvider.GetRoutes(), DefaultProxyConfigProvider.GetClusters());


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

    var result = await runtimeConfigurator.UpdateDefaultProxyConfig();

    if (result.IsFailure)
    {
        // TODO: Throw event to notify the admin frontend!
    }
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseRequestContextLogging();

app.UseSerilogRequestLogging();

app.UseCustomExceptionHandler();

app.MapEndpoints();

app.MapReverseProxy();

await app.RunAsync();

namespace EnvironmentGateway.Api
{
    public partial class Program;
}
