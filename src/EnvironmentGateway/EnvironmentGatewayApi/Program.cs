using System.Reflection;
using EnvironmentGatewayApi;
using EnvironmentGatewayApi.Endpoints.ReverseProxy;
using EnvironmentGatewayApi.Extensions;
using EnvironmentGatewayApi.ReverseProxy;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddPresentation();

builder.Services.AddDbContext<TestDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Database")
    ?? throw new InvalidOperationException("Connection string 'Database' not found.")));

builder.Services.AddReverseProxy()
    .LoadFromMemory(StartupBehavior.GetRoutes(), StartupBehavior.GetClusters());

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
