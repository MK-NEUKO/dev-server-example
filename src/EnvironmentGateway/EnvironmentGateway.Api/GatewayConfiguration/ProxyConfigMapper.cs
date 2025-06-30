using EnvironmentGateway.Application.GatewayConfigs.GetCurrentConfig;
using Yarp.ReverseProxy.Configuration;

namespace EnvironmentGateway.Api.GatewayConfiguration;

internal static class ProxyConfigMapper
{
    public static ProxyConfig Map(CurrentConfigResponse config)
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
        var destinations = new Dictionary<string, DestinationConfig>(StringComparer.OrdinalIgnoreCase);
        config.Clusters.ForEach(cluster =>
        {
            foreach (var keyValuePair in cluster.Destinations)
            {
                var destinationConfig = new DestinationConfig()
                {
                    Address = keyValuePair.Value.Address
                };
                destinations.Add(keyValuePair.Key, destinationConfig);
            }

            var clusterConfig = new ClusterConfig()
            {
                ClusterId = cluster.ClusterName,
                Destinations = destinations
            };
            clusters.Add(clusterConfig);
        });

        return new ProxyConfig(routes.ToArray(), clusters.ToArray());
    }
}
