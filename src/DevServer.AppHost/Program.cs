using System.Diagnostics;
using System.Diagnostics.Tracing;
using Aspire.Hosting;
using Aspire.Hosting.Postgres;
using k8s.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var pgAdmin = builder.AddContainer(
        name: "pgadmin",
        image: "dpage/pgadmin4")
    .WithBindMount(
        source: builder.Configuration.GetValue<string>("PgAdmin:DataBindMount")!,
        target: "/var/lib/pgadmin",
        isReadOnly: false)
    .WithEnvironment("PGADMIN_DEFAULT_EMAIL", builder.Configuration.GetValue<string>("PgAdmin:UserEmail")!)
    .WithEnvironment("PGADMIN_DEFAULT_PASSWORD", builder.Configuration.GetValue<string>("PgAdmin:UserPassword"))
    .WithHttpEndpoint(port: 9105, targetPort: 80, name: "pgadmin", isProxied: false);

var gatewaysDbServer = builder.AddPostgres(
        name: "GatewaysDbServer")
    .WithDataBindMount(
        source: builder.Configuration.GetValue<string>("GatewayDbServer:DataBindMount")!,
        isReadOnly: false)
    .WithHttpEndpoint(port: 9001, name: "GatewaysDb", isProxied: false);

var productionGatewayDb = gatewaysDbServer.AddDatabase("ProductionGatewayDb");

var userManagerDbServer = builder.AddPostgres(
    name: "UserManagerDbServer")
    .WithDataBindMount(
        source: builder.Configuration.GetValue<string>("UserManagerDbServer:DataBindMount")!,
        isReadOnly: false)
    .WithHttpEndpoint(port: 9501, name: "UserManagerDb", isProxied: false);

var userManagerDb = userManagerDbServer.AddDatabase("UserManagerDb");

var keycloak = builder.AddKeycloak("Keycloak", 6001)
    .WithDataBindMount(builder.Configuration.GetValue<string>("Keycloak:DataBindMount")!);

var productionGateway = builder.AddProject<Projects.EnvironmentGateway_Api>("ProductionGateway")
    .WithReference(productionGatewayDb)
    .WaitFor(productionGatewayDb);

var userManagerApi = builder.AddProject<Projects.UserManager_Api>("UserManager")
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
