using EnvironmentGateway.Api.GatewayConfiguration;
using EnvironmentGatewayApi.GatewayConfiguration.Abstractions;
using Yarp.ReverseProxy.Configuration;

namespace EnvironmentGatewayApi.GatewayConfiguration;

internal sealed class RuntimeConfigurator(IInitialConfigurator initialConfigurator,
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
        if (clusters != null)
            foreach (var cluster in clusters)
            {
                var newDestinations = new Dictionary<string, DestinationConfig>(cluster.Destinations!.Count);
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

        if (routes != null) inMemoryConfigProvider.Update(routes, newClusters);
    }

    public async Task InitializeGateway()
    {
        var initialConfiguration = await initialConfigurator.GetInitialConfigurationAsync();

        UpdateConfiguration(initialConfiguration);
    }

    private void UpdateConfiguration(InitialConfiguration initialConfiguration)
    {
        inMemoryConfigProvider.Update(initialConfiguration.Routes, initialConfiguration.Clusters);
    }
}