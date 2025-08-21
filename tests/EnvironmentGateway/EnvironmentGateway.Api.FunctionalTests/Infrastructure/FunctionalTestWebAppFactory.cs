using EnvironmentGateway.Application.Abstractions.Data;
using EnvironmentGateway.Domain.GatewayConfigs;
using EnvironmentGateway.Infrastructure;
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
        .WithUsername("mkneuko")
        .WithPassword("password123")
        .WithResourceMapping(
            new FileInfo(".files/dev-server-example-realm-export.json"),
            new FileInfo("/opt/keycloak/data/import/realm.json"))
        .WithCommand("--import-realm")
        .Build();

    public string KeycloakBaseUrl { get; private set; } = "";

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        Environment.SetEnvironmentVariable("DB_NAME", "test-db");
        
        builder.ConfigureAppConfiguration((context, configBuilder) =>
        {
            var keycloakServiceName = _keycloakContainer.Name; // oder dynamisch aus _keycloakContainer
            var keycloakRealm = "dev-server-example"; // oder dynamisch aus _keycloakContainer
            var keycloakBaseUrl = $"http://{_keycloakContainer.Hostname}:{_keycloakContainer.GetMappedPublicPort(8080)}/";

            var dict = new List<KeyValuePair<string, string>>
            {
                new("Keycloak:ServiceName", keycloakServiceName),
                new("Keycloak:Realm", keycloakRealm),
                new("Keycloak:Issuer", $"{keycloakBaseUrl}realms/dev-server-example"),
                new("Keycloak:AuthorizationUrl", $"{keycloakBaseUrl}realms/dev-server-example/protocol/openid-connect/auth"),
                new("Keycloak:MetadataAddress", $"{keycloakBaseUrl}realms/dev-server-example/.well-known/openid-configuration"),
                // 
            };
            configBuilder.AddInMemoryCollection(dict);
        });
            
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
        });
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
        await _keycloakContainer.StartAsync();
        
        var keycloakHost = _keycloakContainer.Hostname; // or .Host
        var keycloakPort = _keycloakContainer.GetMappedPublicPort(8080); // Keycloak default HTTP port
        KeycloakBaseUrl = $"http://{keycloakHost}:{keycloakPort}/";

        await InitializeTestConfigAsync();
    }
        
    public new async Task DisposeAsync()
    {
        await _dbContainer.StopAsync();
        await _keycloakContainer.StopAsync();
    }
    
    private async Task InitializeTestConfigAsync()
    {
        using var scope = Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<EnvironmentGatewayDbContext>();

        await dbContext.Database.EnsureDeletedAsync();
        await dbContext.Database.EnsureCreatedAsync();

        var initialConfig = GatewayConfig.Create();
        dbContext.GatewayConfigs.Add(initialConfig);

        await dbContext.SaveChangesAsync();
    }
}
