using System.Diagnostics;
using System.Diagnostics.Tracing;
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
        source: builder.Configuration.GetValue<string>("GatewayDbServer:DataBindMount")!,
        isReadOnly: false)
    .WithHttpEndpoint(port: 9001, name: "GatewaysDb", isProxied: false);

var productionGatewayDb = gatewaysDbServer.AddDatabase("ProductionGatewayDb");

var userManagerDbServer = builder.AddPostgres(
    name: "UserManagerDbServer")
    .WithPgAdmin()
    .WithDataBindMount(
        source: builder.Configuration.GetValue<string>("UserManagerDbServer:DataBindMount")!,
        isReadOnly: false)
    .WithHttpEndpoint(port: 9501, name: "UserManagerDb", isProxied: false);

var userManagerDb = userManagerDbServer.AddDatabase("UserManagerDb");

var keycloak = builder.AddKeycloak("Keycloak", 6001)
    .WithDataBindMount(builder.Configuration.GetValue<string>("Keycloak:DataBindMount")!);

var productionGateway = builder.AddProject<Projects.EnvironmentGateway_Api>("ProductionGateway")
    .WithScalar()
    .WithReference(productionGatewayDb)
    .WaitFor(productionGatewayDb);

var userManagerApi = builder.AddProject<Projects.UserManager_Api>("UserManager")
    .WithScalar()
    .WithReference(userManagerDb)
    .WaitFor(userManagerDb);

builder.AddNpmApp("serviceFrontend", "../service-frontend")
    .WaitFor(keycloak)
    .WaitFor(userManagerApi)
    .WithHttpEndpoint(port: 4200, name: "service-frontend", isProxied:false, env: "SERVICE_FRONTEND_PORT")
    .WithExternalHttpEndpoints();

builder.AddNpmApp("adminFrontend", "../admin-frontend")
    .WithReference(productionGateway)
    .WaitFor(productionGateway)
    .WithHttpEndpoint(port: 4300, name: "service-frontend", isProxied:false, env: "ADMIN_FRONTEND_PORT")
    .WithExternalHttpEndpoints();



await builder.Build().RunAsync();
