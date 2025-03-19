using EnvironmentGatewayApi.GatewayConfiguration.Abstractions;
using Yarp.ReverseProxy.Configuration;

namespace EnvironmentGatewayApi.GatewayConfiguration;

public class RuntimeConfigurator(
    IProxyConfigProvider configurationProvider,
    InMemoryConfigProvider inMemoryConfigProvider) 
    : IRuntimeConfigurator
{
    public void ChangeDestinationAddress(string address)
    {
        var currentConfig = configurationProvider.GetConfig();
        var routes = currentConfig?.Routes;
        var clusters = currentConfig?.Clusters;

        var newClusters = new List<ClusterConfig>();
        foreach (var cluster in clusters)
        {
            var newDestinations = new Dictionary<string, DestinationConfig>(cluster.Destinations.Count);
            foreach (var destination in cluster.Destinations)
            {
                var newDestination = new DestinationConfig
                {
                    Address = address
                };
                newDestinations[destination.Key] = newDestination;
            }

            var newCluster = new ClusterConfig
            {
                ClusterId = cluster.ClusterId,
                Destinations = newDestinations
            };
            newClusters.Add(newCluster);
        }

        inMemoryConfigProvider.Update(routes, newClusters);
    }


}