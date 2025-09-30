using EnvironmentGateway.Application.Abstractions.Data;
using EnvironmentGateway.Application.Abstractions.Email;
using EnvironmentGateway.Domain.Abstractions;
using EnvironmentGateway.Domain.Clusters;
using EnvironmentGateway.Domain.Clusters.Destinations;
using EnvironmentGateway.Domain.GatewayConfigs;
using EnvironmentGateway.Infrastructure.Authentication;
using EnvironmentGateway.Infrastructure.Data;
using EnvironmentGateway.Infrastructure.DomainEvents;
using EnvironmentGateway.Infrastructure.Email;
using EnvironmentGateway.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace EnvironmentGateway.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration,
        IHostEnvironment environment) => services
            .AddServices()
            .AddDatabase(configuration)
            .AddAuthenticationInternal(configuration, environment);
    
    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient<IEmailService, EmailService>();
        services.AddTransient<IDomainEventsDispatcher, DomainEventsDispatcher>();

        return services;
    }
    
    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var dbName = configuration["DB_NAME"];

        var connectionString = configuration.GetConnectionString(dbName ?? throw new InvalidOperationException("DB_NAME environment variable is required but not provided")) ??
                               throw new ArgumentException(nameof(configuration));

        //var connectionString = "test";

        services.AddDbContext<EnvironmentGatewayDbContext>(options =>
        {
            options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();
        });

        services.AddScoped<IGatewayConfigRepository, GatewayConfigRepository>();

        services.AddScoped<IClusterRepository, ClusterRepository>();
        
        services.AddScoped<IDestinationRepository, DestinationRepository>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<EnvironmentGatewayDbContext>());

        services.AddScoped<IEnvironmentGatewayDbContext>(sp => sp.GetRequiredService<EnvironmentGatewayDbContext>());

        services.AddSingleton<ISqlConnectionFactory>(_ =>
            new SqlConnectionFactory(connectionString));

        return services;
    }

    public static IServiceCollection AddAuthenticationInternal(
        this IServiceCollection services,
        IConfiguration configuration,
        IHostEnvironment environment)
    {
        var keycloakOptions = configuration.GetSection("Keycloak").Get<KeycloakOptions>();
        ArgumentNullException.ThrowIfNull(keycloakOptions);

        services.AddAuthentication()
            .AddKeycloakJwtBearer(
                serviceName: keycloakOptions.ServiceName, realm: keycloakOptions.Realm);

        services.Configure<AuthenticationOptions>(configuration.GetSection("JwtBearer"));

        services.ConfigureOptions<JwtBearerOptionsSetup>();

        services.AddAuthorization();
        
        return services;
    }
}
