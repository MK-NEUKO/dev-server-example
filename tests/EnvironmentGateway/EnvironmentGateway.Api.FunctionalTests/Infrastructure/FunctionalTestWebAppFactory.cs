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

    private readonly KeycloakContainer _keycloakContainer = new KeycloakBuilder()
        .WithImage("quay.io/keycloak/keycloak:21.1")
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
            
            
        });
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();

        await InitializeTestConfigAsync();
    }

    public new async Task DisposeAsync()
    {
        await _dbContainer.StopAsync();
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
