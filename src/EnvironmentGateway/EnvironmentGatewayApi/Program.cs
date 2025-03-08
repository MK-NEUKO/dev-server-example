using System.Reflection;
using EnvironmentGatewayApi;
using EnvironmentGatewayApi.Endpoints.ReverseProxy;
using EnvironmentGatewayApi.Extensions;
using EnvironmentGatewayApi.ReverseProxy;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddReverseProxy()
    .LoadFromMemory(StartupBehavior.GetRoutes(), StartupBehavior.GetClusters());

builder.Services.AddPresentation();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapEndpoints();
app.MapReverseProxy();

await app.RunAsync();
