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

var keycloakDbServer = builder.AddPostgres(
    name: "KeycloakDbServer")
    .WithDataBindMount(
        source: builder.Configuration.GetValue<string>("KeycloakDbServer:DataBindMount")!,
        isReadOnly: false)
    .WithHttpsEndpoint(port: 6010, name: "KeycloakDbServerHttp", isProxied: false);

var keycloakDb = keycloakDbServer.AddDatabase("KeycloakDb");

var gatewaysDbServer = builder.AddPostgres(
        name: "GatewaysDbServer")
    .WithDataBindMount(
        source: builder.Configuration.GetValue<string>("GatewayDbServer:DataBindMount")!,
        isReadOnly: false)
    .WithHttpsEndpoint(port: 9001, name: "GatewaysDbServerHttp", isProxied: false);

var productionGatewayDb = gatewaysDbServer.AddDatabase("ProductionGatewayDb");
var customerGatewayDb = gatewaysDbServer.AddDatabase("CustomerGatewayDb");

var userManagerDbServer = builder.AddPostgres(
    name: "UserManagerDbServer")
    .WithDataBindMount(
        source: builder.Configuration.GetValue<string>("UserManagerDbServer:DataBindMount")!,
        isReadOnly: false)
    .WithHttpsEndpoint(port: 9501, name: "UserManagerDbServerHttp", isProxied: false);

var userManagerDb = userManagerDbServer.AddDatabase("UserManagerDb");

#endregion


#region AuthConfig

var keycloak = builder.AddKeycloak("Keycloak", 6001)
    .WithDataBindMount(builder.Configuration.GetValue<string>("Keycloak:DataBindMount")!)
    .WaitFor(keycloakDb)
    .WithEnvironment("KC_DB", "postgres")
    .WithEnvironment("KC_DB_URL", "jdbc:postgresql://KeycloakDbServer:5432/KeycloakDb")
    .WithEnvironment("KC_DB_USERNAME", "postgres")
    .WithEnvironment("KC_DB_PASSWORD", builder.Configuration.GetValue<string>("Parameters:KeycloakDbServer-password"));

#endregion



#region ApiConfig

var productionGateway = builder.AddProject<Projects.EnvironmentGateway_Api>("ProductionGateway")
    .WithReference(productionGatewayDb)
    .WithEnvironment("DB_NAME", nameof(productionGatewayDb))
    .WithEnvironment("ASPNETCORE_ENVIRONMENT", "Development")
    .WaitFor(productionGatewayDb)
    .WaitFor(keycloak)
    .WithHttpsEndpoint(port: 9100, name: "ProductionGatewayHttps", isProxied: false)
    .WithUrlForEndpoint("ProductionGatewayHttps", url => url.Url = "/service1")
    .WithHttpsEndpoint(port: 9102, name: "ProductionGatewayHttpsScalar", isProxied: false)
    .WithUrlForEndpoint("ProductionGatewayHttpsScalar", url => url.Url = "/scalar" );

var customerGateway = builder.AddProject<Projects.EnvironmentGateway_Api>("CustomerGateway")
    .WithReference(customerGatewayDb)
    .WithEnvironment("DB_NAME", nameof(customerGatewayDb))
    .WithEnvironment("ASPNETCORE_ENVIRONMENT", "Development")
    .WaitFor(customerGatewayDb)
    .WaitFor(keycloak)
    .WithHttpsEndpoint(port: 9110, name: "CustomerGatewayHttp", isProxied: false)
    .WithHttpsEndpoint(port: 9112, name: "CustomerGatewayHttps", isProxied: false)
    .WithUrlForEndpoint("ProductionGatewayHttpsScalar", url => url.Url = "/scalar" );

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
