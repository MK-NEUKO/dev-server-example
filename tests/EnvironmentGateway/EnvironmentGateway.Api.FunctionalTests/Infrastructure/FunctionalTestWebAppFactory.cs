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
using System.Net.Http.Json;
using EnvironmentGateway.Application.GatewayConfigs.CreateNewConfig;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.DataCollection;
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
    
    //private readonly PostgreSqlContainer _dbContainerKeycloak = new PostgreSqlBuilder()
    //    .WithImage("postgres:latest")
    //    .WithName("keycloakDBServer")
    //    .WithDatabase("keycloak")
    //    .WithUsername("testUser")
    //    .WithPassword("password")
    //    .WithNetworkAliases("keycloakDbServer")
    //    .Build();
    
    
    private readonly KeycloakContainer _keycloakContainer = new KeycloakBuilder()
        .WithImage("keycloak/keycloak:latest")
        .WithUsername("mkneuko")
        .WithPassword("password123")
        //.WithEnvironment("KC_DB", "postgres")
        //.WithEnvironment("KC_DB_URL", "jdbc:postgresql://172.17.0.4:5432/keycloak")
        //.WithEnvironment("KC_DB_USERNAME", "testUser")
        //.WithEnvironment("KC_DB_PASSWORD", "password") 
        .WithResourceMapping(
            new FileInfo(".files/dev-server-example-realm-export.json"),
            new FileInfo("/opt/keycloak/data/import/realm.json"))
        .WithCommand("--import-realm")
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        Environment.SetEnvironmentVariable("DB_NAME", "test-db");
        
        
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
            
            services.AddAuthentication()
                .AddKeycloakJwtBearer(
                    serviceName: _keycloakContainer.Name,
                    realm: "dev-server-example",
                    configureOptions: options =>
                    {
                        options.RequireHttpsMetadata = false;
                    });

        });
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
        //await _dbContainerKeycloak.StartAsync();
        await _keycloakContainer.StartAsync();
        await InitializeTestConfigAsync();
    }

    public new async Task DisposeAsync()
    {
        await _dbContainer.StopAsync();
        await _keycloakContainer.StopAsync();
        //await _dbContainerKeycloak.StopAsync();
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
