using Yarp.ReverseProxy.Configuration;

namespace EnvironmentGatewayApi.GatewayConfiguration;

internal class InitialConfigurator
{
    public static InitialConfiguration GetInitialConfiguration()
    {
        // Query a database to get the initial configuration.

        // If the database has no configuration or is not available,
        // get a default configuration from domain.


        // Apply configuration to gateway.

        // Save configuration to database.


        // For now, return a default configuration.
        var initialConfig = new InitialConfiguration
        {
            Routes = GetRoutes(),
            Clusters = GetClusters()
        };

        return initialConfig;
    }

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

    private static ClusterConfig[] GetClusters()
    {
        return
        [
            new ClusterConfig()
            {
                ClusterId = "cluster1",
                Destinations = new Dictionary<string, DestinationConfig>(StringComparer.OrdinalIgnoreCase)
                {
                    { "destination1", new DestinationConfig() { Address = "https://neuko-know-how.com" } },
                }
            }
        ];
    }
}