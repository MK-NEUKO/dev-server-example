using System.Reflection;
using DevServer.ServiceDefaults;
using EnvironmentGateway.Api;
using EnvironmentGateway.Api.Extensions;
using EnvironmentGateway.Api.GatewayConfiguration.Abstractions;
using EnvironmentGateway.Application;
using EnvironmentGateway.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddOpenApiWithAuth(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            .WithOrigins("http://localhost:4300") // AdminFrontend-Port
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

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

app.MapReverseProxy();

await app.RunAsync();

namespace EnvironmentGateway.Api
{
    public partial class Program;
}
