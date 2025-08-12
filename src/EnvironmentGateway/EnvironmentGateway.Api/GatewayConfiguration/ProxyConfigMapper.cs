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
            var Transforms = new List<Dictionary<string, string>>();
            foreach (var transform in route.Transforms.Transforms)
            {
                Transforms.Add(transform);
            }
            var routeConfig = new RouteConfig()
            {
                RouteId = route.RouteName,
                ClusterId = route.ClusterName,
                Match = new RouteMatch()
                {
                    Path = route.Match.Path
                },
                Transforms = Transforms
                    
            };
            routes.Add(routeConfig);
        }

        var clusters = new List<ClusterConfig>();
        config.Clusters.ForEach(cluster =>
        {
            var destinations = new Dictionary<string, DestinationConfig>(StringComparer.OrdinalIgnoreCase);
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
