using System.Diagnostics;
using DevServer.AppHost;
using k8s.Models;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Projects;

var builder = DistributedApplication.CreateBuilder(args);


var envGateway = builder.AddProject<Projects.EnvironmentGateway_Api>("envGateway")
    .WithScalar()
    .WithExternalHttpEndpoints();


builder.AddNpmApp("adminFrontend","../admin-frontend")
    .WithReference(envGateway)
    .WaitFor(envGateway)
    .WithHttpEndpoint(env: "ADMIN_FRONTEND_PORT")
    .WithExternalHttpEndpoints()
    .PublishAsDockerFile();

await builder.Build().RunAsync();
