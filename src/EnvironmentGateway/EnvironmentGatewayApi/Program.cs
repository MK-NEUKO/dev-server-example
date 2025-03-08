using System.Reflection;
using EnvironmentGatewayApi;
using EnvironmentGatewayApi.Endpoints.ReverseProxy;
using EnvironmentGatewayApi.Extensions;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddReverseProxy()
    .LoadFromMemory(DefaultConfiguration.GetRoutes(), DefaultConfiguration.GetClusters());

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
