using System.Reflection;
using EnvironmentGateway.Api;
using EnvironmentGateway.Api.Extensions;
using EnvironmentGateway.Api.GatewayConfiguration.Abstractions;
using EnvironmentGateway.Application;
using EnvironmentGateway.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddOpenApiWithAuth(builder.Configuration);

builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy
    .WithOrigins("http://localhost:4300")
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials()));

builder.Services
    .AddApplication()
    .AddPresentation()
    .AddInfrastructure(builder.Configuration);

builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());
    
var app = builder.Build();

app.UseCors();

app.MapDefaultEndpoints();

app.MapEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseScalarWithUi();

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

app.UseHttpsRedirection();

app.UseRequestContextLogging();

app.UseCustomExceptionHandler();

app.UseAuthentication();

app.UseAuthorization();

app.MapReverseProxy(proxyPipeline =>
{
    proxyPipeline.Use(async (context, next) =>
    {
        context.Response.Headers["Access-Control-Allow-Origin"] = "http://localhost:4300";
        context.Response.Headers["Access-Control-Allow-Headers"] = "Content-Type, Authorization";
        context.Response.Headers["Access-Control-Allow-Methods"] = "GET, POST, PUT, DELETE, OPTIONS";
        context.Response.Headers["Access-Control-Allow-Credentials"] = "true";
        await next();
    });

    proxyPipeline.UseAuthentication();
});

await app.RunAsync();

namespace EnvironmentGateway.Api
{
    public partial class Program;
}
