using System.Diagnostics;
using DevServer.AppHost;
using k8s.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var gatewaysDbServer = builder.AddPostgres(
        name: "GatewaysDbServer")
    .WithPgAdmin()
    .WithDataBindMount(
        source: builder.Configuration.GetValue<string>("GatewayDbServer:GatewayDbServerVolume")!,
        isReadOnly: false)
    .WithHttpEndpoint(port: 9001, name: "GatewaysDb", isProxied: false);

var productionGatewayDb = gatewaysDbServer.AddDatabase("ProductionGatewayDb");

var productionGateway = builder.AddProject<Projects.EnvironmentGateway_Api>("ProductionGateway")
    .WithScalar()
    .WithReference(productionGatewayDb)
    .WaitFor(productionGatewayDb)
    .WithExternalHttpEndpoints();


builder.AddNpmApp("adminFrontend","../admin-frontend")
    .WithReference(productionGateway)
    .WaitFor(productionGateway)
    .WithHttpEndpoint(env: "ADMIN_FRONTEND_PORT")
    .WithExternalHttpEndpoints()
    .PublishAsDockerFile();

await builder.Build().RunAsync();
