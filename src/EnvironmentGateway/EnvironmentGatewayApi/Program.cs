using EnvironmentGatewayApi;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddReverseProxy()
    .LoadFromMemory(DefaultConfiguration.GetRoutes(), DefaultConfiguration.GetClusters());

builder.Services.AddControllers();

builder.Services.AddOpenApi();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapReverseProxy();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
