using EnvironmentGateway.Application.Abstractions.Data;
using EnvironmentGateway.Application.Abstractions.Email;
using EnvironmentGateway.Domain.Abstractions;
using EnvironmentGateway.Domain.Destinations;
using EnvironmentGateway.Domain.GatewayConfigs;
using EnvironmentGateway.Infrastructure.Data;
using EnvironmentGateway.Infrastructure.DomainEvents;
using EnvironmentGateway.Infrastructure.Email;
using EnvironmentGateway.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace EnvironmentGateway.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration) => 
        services
            .AddServices()
            .AddDatabase(configuration)
            .AddAuthenticationInternal(configuration);
    
    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient<IEmailService, EmailService>();
        services.AddTransient<IDomainEventsDispatcher, DomainEventsDispatcher>();

        return services;
    }
    
    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("ProductionGatewayDb") ??
                               throw new ArgumentException(nameof(configuration));
        
        services.AddDbContext<EnvironmentGatewayDbContext>(options =>
        {
            options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();
        });

        services.AddScoped<IGatewayConfigRepository, GatewayConfigRepository>();
        
        services.AddScoped<IDestinationRepository, DestinationRepository>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<EnvironmentGatewayDbContext>());

        services.AddScoped<IEnvironmentGatewayDbContext>(sp => sp.GetRequiredService<EnvironmentGatewayDbContext>());

        services.AddSingleton<ISqlConnectionFactory>(_ =>
            new SqlConnectionFactory(connectionString));

        return services;
    }

    public static IServiceCollection AddAuthenticationInternal(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.Audience = configuration["Keycloak:Audience"];
                o.MetadataAddress = configuration["Keycloak:MetadataAddress"]!;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = configuration["Keycloak:Issuer"],
                };
            });
        services.AddAuthentication();

        return services;
    }
}
