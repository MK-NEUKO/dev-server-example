using Yarp.ReverseProxy.Configuration;

namespace EnvironmentGatewayApi;

internal static class DefaultConfiguration
{
    const string DEBUG_HEADER = "Debug";
    const string DEBUG_METADATA_KEY = "debug";
    const string DEBUG_VALUE = "true";

    internal static RouteConfig[] GetRoutes()
    {
        return
        [
            new RouteConfig()
            {
                RouteId = "route" + Random.Shared.Next(), // Forces a new route id each time GetRoutes is called.
                ClusterId = "cluster1",
                Match = new RouteMatch
                {
                    // Path or Hosts are required for each route. This catch-all pattern matches all request paths.
                    Path = "{**catch-all}"
                }
            }
        ];
    }

    public static ClusterConfig[] GetClusters()
    {
        var debugMetadata = new Dictionary<string, string>
        {
            { DEBUG_METADATA_KEY, DEBUG_VALUE }
        };

        return
        [
            new ClusterConfig()
            {
                ClusterId = "cluster1",
                SessionAffinity = new SessionAffinityConfig { Enabled = true, Policy = "Cookie", AffinityKeyName = ".Yarp.ReverseProxy.Affinity" },
                Destinations = new Dictionary<string, DestinationConfig>(StringComparer.OrdinalIgnoreCase)
                {
                    { "destination1", new DestinationConfig() { Address = "https://neuko-know-how.com" } },
                    { "debugdestination1", new DestinationConfig() {
                        Address = "https://bing.com",
                        Metadata = debugMetadata  }
                    },
                }
            }
        ];
    }
    
}