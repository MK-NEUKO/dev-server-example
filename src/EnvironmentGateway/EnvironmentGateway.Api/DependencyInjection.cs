using EnvironmentGateway.Api.GatewayConfiguration;
using EnvironmentGateway.Api.GatewayConfiguration.Abstractions;

namespace EnvironmentGateway.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        //services.AddEndpointsApiExplorer();
        

        services.AddScoped<IRuntimeConfigurator, RuntimeConfigurator>();
        services.AddScoped<ICurrentConfigProvider, CurrentConfigProvider>();

        services.AddReverseProxy()
            .LoadFromMemory(DefaultProxyConfigProvider.GetRoutes(), DefaultProxyConfigProvider.GetClusters());

        return services;
    }
}
