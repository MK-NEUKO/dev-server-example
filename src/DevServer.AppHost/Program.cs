using System.Diagnostics;
using DevServer.AppHost;
using k8s.Models;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = DistributedApplication.CreateBuilder(args);


builder.AddProject<Projects.EnvironmentGatewayApi>("EnvironmentGatewayApi")
    .WithScalar();



var weatherApi = builder.AddProject<Projects.WeatherForecastApi>("weatherApi")
    .WithExternalHttpEndpoints();

builder.AddNpmApp("devServerClient", "../DevServerClient")
    .WithReference(weatherApi)
    .WaitFor(weatherApi)
    .WithHttpEndpoint(env: "DEV_SERVER_CLIENT_PORT")
    .WithExternalHttpEndpoints()
    .PublishAsDockerFile();




await builder.Build().RunAsync();
