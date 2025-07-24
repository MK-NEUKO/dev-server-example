using EnvironmentGateway.Domain.Abstractions;
using EnvironmentGateway.Domain.Destinations;
using EnvironmentGateway.Domain.GatewayConfigs;
using EnvironmentGateway.Domain.Shared;

namespace EnvironmentGateway.Domain.Clusters;

public sealed class Cluster : Entity
{
    private Cluster(Guid id, Name clusterName, List<Destination> destinations)
        : base(id)
    {
        ClusterName = clusterName;
        foreach (Destination destination in destinations)
        {
            Destinations.Add(destination);
        }
    }

    private Cluster()
    {
    }

    public Guid GatewayConfigId { get; init; }
    public GatewayConfig GatewayConfig { get; init; } = null!;
    public Name ClusterName { get; init; } = new Name("cluster1");
    public List<Destination> Destinations { get; } = [];

    public static Cluster CreateNewCluster(string clusterName, int index)
    {
        var initialDestinations = new List<Destination>();
        var addresses = new List<string>() { "https://example.com", "https://neuko-know-how.com", "https://cneb.de" };
        for (var i = 0; i <= 2; i++)
        {
            var destination = Destination.Create($"destination{i+1}{index}", addresses[i]);
            initialDestinations.Add(destination);
        }

        var initialCluster = new Cluster(Guid.NewGuid(), new Name(clusterName), initialDestinations);

        return initialCluster;
    }
}
