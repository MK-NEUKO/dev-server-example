using EnvironmentGateway.Application.GatewayConfigs.GetStartConfig;
using Yarp.ReverseProxy.Configuration;

namespace EnvironmentGateway.Api.GatewayConfiguration;

internal static class ProxyConfigMapper
{
    public static ProxyConfig Map(StartConfigResponse config)
    {
        var routes = new List<RouteConfig>();
        foreach (var route in config.Routes)
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
        foreach (var cluster in config.Clusters)
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

        return new ProxyConfig(routes.ToArray(), clusters.ToArray());
    }
}