using System.Diagnostics;
using System.Diagnostics.Tracing;
using Aspire.Hosting;
using Aspire.Hosting.Postgres;
using k8s.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Projects;

var builder = DistributedApplication.CreateBuilder(args);

#region DatabaseAdmin

var pgAdmin = builder.AddContainer(
name: "PgAdmin",
image: "dpage/pgadmin4")
.WithBindMount(
source: builder.Configuration.GetValue<string>("PgAdmin:DataBindMount")!,
target: "/var/lib/pgadmin",
isReadOnly: false)
.WithEnvironment("PGADMIN_DEFAULT_EMAIL", builder.Configuration.GetValue<string>("PgAdmin:UserEmail")!)
.WithEnvironment("PGADMIN_DEFAULT_PASSWORD", builder.Configuration.GetValue<string>("PgAdmin:UserPassword"))
.WithHttpEndpoint(port: 9105, targetPort: 80, name: "PgAdminHttp", isProxied: false);

#endregion


#region DatabaseConfig

var gatewaysDbServer = builder.AddPostgres(
        name: "GatewaysDbServer")
    .WithDataBindMount(
        source: builder.Configuration.GetValue<string>("GatewayDbServer:DataBindMount")!,
        isReadOnly: false)
    .WithHttpEndpoint(port: 9001, name: "GatewaysDbServerHttp", isProxied: false);

var productionGatewayDb = gatewaysDbServer.AddDatabase("ProductionGatewayDb");
var customerGatewayDb = gatewaysDbServer.AddDatabase("CustomerGatewayDb");

var userManagerDbServer = builder.AddPostgres(
    name: "UserManagerDbServer")
    .WithDataBindMount(
        source: builder.Configuration.GetValue<string>("UserManagerDbServer:DataBindMount")!,
        isReadOnly: false)
    .WithHttpEndpoint(port: 9501, name: "UserManagerDbServerHttp", isProxied: false);

var userManagerDb = userManagerDbServer.AddDatabase("UserManagerDb");

#endregion


#region AuthConfig

var keycloak = builder.AddKeycloak("Keycloak", 6001)
    .WithDataBindMount(builder.Configuration.GetValue<string>("Keycloak:DataBindMount")!);

#endregion



#region ApiConfig

var productionGateway = builder.AddProject<Projects.EnvironmentGateway_Api>("ProductionGateway")
    .WithReference(productionGatewayDb)
    .WithEnvironment("DB_NAME", nameof(productionGatewayDb))
    .WithEnvironment("ASPNETCORE_ENVIRONMENT", "Development")
    .WaitFor(productionGatewayDb)
    .WaitFor(keycloak)
    .WithHttpEndpoint(port: 9100, name: "ProductionGatewayHttp", isProxied: false)
    .WithHttpsEndpoint(port: 9101, name: "ProductionGatewayHttps", isProxied: false);

var customerGateway = builder.AddProject<Projects.EnvironmentGateway_Api>("CustomerGateway")
    .WithReference(customerGatewayDb)
    .WithEnvironment("DB_NAME", nameof(customerGatewayDb))
    .WithEnvironment("ASPNETCORE_ENVIRONMENT", "Development")
    .WaitFor(customerGatewayDb)
    .WaitFor(keycloak)
    .WithHttpEndpoint(port: 9200, name: "CustomerGatewayHttp", isProxied: false)
    .WithHttpsEndpoint(port: 9201, name: "CustomerGatewayHttps", isProxied: false);

var userManagerApi = builder.AddProject<Projects.UserManager_Api>("UserManager")
    .WithReference(userManagerDb)
    .WaitFor(userManagerDb)
    .WaitFor(keycloak);

#endregion



#region FrontendConfig

builder.AddNpmApp("ServiceFrontend", "../service-frontend")
    .WaitFor(userManagerApi)
    .WaitFor(keycloak)
    .WithHttpEndpoint(port: 4200, name: "ServiceFrontendHttp", isProxied: false, env: "SERVICE_FRONTEND_PORT")
    .WithExternalHttpEndpoints();

builder.AddNpmApp("AdminFrontend", "../admin-frontend")
    .WaitFor(productionGateway)
    .WaitFor(customerGateway)
    .WaitFor(keycloak)
    .WithHttpEndpoint(port: 4300, name: "AdminFrontendHttp", isProxied: false, env: "ADMIN_FRONTEND_PORT")
    .WithExternalHttpEndpoints();

#endregion


await builder.Build().RunAsync();
