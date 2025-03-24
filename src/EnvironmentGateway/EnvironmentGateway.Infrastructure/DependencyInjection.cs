using EnvironmentGateway.Application.Abstractions.Email;
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

        return services;
    }
}