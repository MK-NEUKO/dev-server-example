using EnvironmentGateway.Application.Abstractions.Data;
using EnvironmentGateway.Domain.GatewayConfigs;
using EnvironmentGateway.Infrastructure;
using EnvironmentGateway.Infrastructure.Authentication;
using EnvironmentGateway.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Configuration;
using Testcontainers.PostgreSql;
using Testcontainers.Keycloak;

namespace EnvironmentGateway.Api.FunctionalTests.Infrastructure;

public class FunctionalTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithImage("postgres:latest")
        .WithDatabase("test-db")
        .WithUsername("testUser")
        .WithPassword("password")
        .Build();
    
    private readonly KeycloakContainer _keycloakContainer = new KeycloakBuilder()
        .WithImage("keycloak/keycloak:latest")
        .WithUsername("testUser")
        .WithPassword("password")
        .WithResourceMapping(
            new FileInfo(".files/dev-server-example-realm-export.json"),
            new FileInfo("/opt/keycloak/data/import/realm.json"))
        .WithCommand("--import-realm")
        .Build();

    public string KeycloakBaseUrl { get; private set; } = "";

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        Environment.SetEnvironmentVariable("DB_NAME", "test-db");
        
        KeycloakBaseUrl = $"http://{_keycloakContainer.Hostname}:{_keycloakContainer.GetMappedPublicPort(8080)}";

        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<EnvironmentGatewayDbContext>));
            services.AddDbContext<EnvironmentGatewayDbContext>(options =>
                options
                    .UseNpgsql(_dbContainer.GetConnectionString())
                    .UseSnakeCaseNamingConvention());

            services.RemoveAll(typeof(ISqlConnectionFactory));
            services.AddSingleton<ISqlConnectionFactory>(_ =>
                new SqlConnectionFactory(_dbContainer.GetConnectionString()));

            services.Configure<KeycloakOptions>(options =>
            {
                options.ServiceName = _keycloakContainer.Name;
                options.Realm = "dev-server-example";
            });
            
            services.Configure<AuthenticationOptions>(options =>
            {
                options.AuthorizationUrl = $"{KeycloakBaseUrl}/realms/dev-server-example/protocol/openid-connect/auth";
                options.Issuer = $"{KeycloakBaseUrl}/realms/dev-server-example";
                options.MetadataAddress =
                    $"{KeycloakBaseUrl}/realms/dev-server-example/.well-known/openid-configuration";
            });
        });
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
        await _keycloakContainer.StartAsync();
    }
        
    public new async Task DisposeAsync()
    {
        await _dbContainer.StopAsync();
        await _keycloakContainer.StopAsync();
    }
}
