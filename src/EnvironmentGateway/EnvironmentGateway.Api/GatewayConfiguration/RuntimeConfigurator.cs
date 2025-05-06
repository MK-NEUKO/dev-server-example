using EnvironmentGateway.Api.GatewayConfiguration.Abstractions;
using EnvironmentGateway.Application.GatewayConfigs.GetStartConfig;
using EnvironmentGateway.Domain.GatewayConfigs;
using MediatR;
using Yarp.ReverseProxy.Configuration;

namespace EnvironmentGateway.Api.GatewayConfiguration;

internal sealed class RuntimeConfigurator(
    IInitialConfigurator initialConfigurator,
    IProxyConfigProvider configurationProvider,
    InMemoryConfigProvider inMemoryConfigProvider,
    ISender sender) 
    : IRuntimeConfigurator
{
    public async Task UpdateConfig()
    {
        var query = new GetStartConfigQuery(true);
        var result = await sender.Send(query, CancellationToken.None);

        var currentConfig = MapToProxyConfig(result.Value);

        UpdateConfiguration(currentConfig);
    }

    private InitialConfiguration MapToProxyConfig(StartConfigResponse resultValue)
    {
        var routes = new List<RouteConfig>();
        foreach (var route in resultValue.Routes)
        {
            var routeConfig = new RouteConfig()
            {
                RouteId = route.RouteName,
                ClusterId = route.ClusterName,
                Match = new RouteMatch()
                {
                    Path = route.Match.Path
                }
            };
            routes.Add(routeConfig);
        }

        var clusters = new List<ClusterConfig>();
        var clusterCounter = 0;
        foreach (var cluster in resultValue.Clusters)
        {
            var clusterConfig = new ClusterConfig()
            {
                ClusterId = cluster.ClusterName,
                Destinations = new Dictionary<string, DestinationConfig>(StringComparer.OrdinalIgnoreCase)
                {
                    {
                        cluster.Destinations[clusterCounter].DestinationName,
                        new DestinationConfig() { Address = cluster.Destinations[clusterCounter].Address }

                    }
                }
            };
            clusterCounter++;
            clusters.Add(clusterConfig);
        }

        return new InitialConfiguration(routes.ToArray(), clusters.ToArray());
    }

    public async Task InitializeGateway()
    {
        var initialConfiguration = await initialConfigurator.GetInitialConfigurationAsync();

        if (initialConfiguration.Routes.IsNullOrEmpty() || initialConfiguration.Clusters.IsNullOrEmpty())
        {
            return;
        }

        UpdateConfiguration(initialConfiguration);
    }

    private void UpdateConfiguration(InitialConfiguration configuration)
    {
        inMemoryConfigProvider.Update(configuration.Routes, configuration.Clusters);
    }
}