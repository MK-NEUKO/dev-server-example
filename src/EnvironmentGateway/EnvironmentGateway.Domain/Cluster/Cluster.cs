using EnvironmentGateway.Domain.Abstractions;
using EnvironmentGateway.Domain.GatewayConfig;

namespace EnvironmentGateway.Domain.Cluster;

public sealed class Cluster : Entity
{
    private Cluster(Guid id)
        : base(id)
    {
    }

    private Cluster(Guid id, Name clusterName)
        : base(id)
    {
        ClusterName = clusterName;
    }

    public Guid GatewayConfigId { get;private set; }
    public Name ClusterName { get; private set; }
    public List<Destination> Destinations { get; private set; } = new();

    public static Cluster CreateInitialCluster(string clusterName, string address)
    {
        var initialCluster = new Cluster(Guid.NewGuid(), new Name(clusterName));
        var initialDestination = Destination.CreateInitialDestination("destination1", address);
        initialCluster.Destinations.Add(initialDestination);
        return initialCluster;
    }
}