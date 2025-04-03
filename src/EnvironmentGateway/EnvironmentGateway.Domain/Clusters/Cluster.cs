using EnvironmentGateway.Domain.Abstractions;
using EnvironmentGateway.Domain.GatewayConfigs;
using EnvironmentGateway.Domain.Shared;

namespace EnvironmentGateway.Domain.Clusters;

public sealed class Cluster : Entity
{
    private Cluster(Guid id, Name clusterName, Destination destination)
        : base(id)
    {
        ClusterName = clusterName;
        Destinations.Add(destination);
    }

    private Cluster()
    {
    }

    public Guid GatewayConfigId { get; init; }
    public GatewayConfig GatewayConfig { get; init; } = null!;
    public Name ClusterName { get; init; } = new Name("cluster1");
    public List<Destination> Destinations { get; } = [];

    public static Cluster CreateInitialCluster(string clusterName, string address)
    {
        var initialDestination = Destination.CreateInitialDestination("destination1", address);

        var initialCluster = new Cluster(Guid.NewGuid(), new Name(clusterName), initialDestination);

        return initialCluster;
    }
}