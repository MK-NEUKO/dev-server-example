using EnvironmentGateway.Application.Abstractions.Email;
using EnvironmentGateway.Domain.Abstractions;
using EnvironmentGateway.Domain.GatewayConfig;
using EnvironmentGateway.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace EnvironmentGateway.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddTransient<IEmailService, IEmailService>();

        var connenctionString = configuration.GetConnectionString("EnvironmentGateway") ??
                                throw new ArgumentException(nameof(configuration));

        services.AddDbContext<EnvironmentGatewayDbContext>(options =>
        {
            options.UseNpgsql().UseSnakeCaseNamingConvention();
        });

        services.AddScoped<IGatewayConfigRepository, GatewayConfigRepository>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<EnvironmentGatewayDbContext>());

        return services;
    }
}