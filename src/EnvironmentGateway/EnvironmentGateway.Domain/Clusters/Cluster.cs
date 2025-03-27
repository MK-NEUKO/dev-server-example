using EnvironmentGateway.Domain.Abstractions;
using EnvironmentGateway.Domain.GatewayConfig;

namespace EnvironmentGateway.Domain.Clusters;

public sealed class Cluster : Entity
{
    private Cluster()
    {
    }

    private Cluster(Guid id, Name clusterName, Destination destination)
        : base(id)
    {
        ClusterName = clusterName;
        Destinations.Add(destination);
    }

    public Guid GatewayConfigId { get;private set; }
    public Name ClusterName { get; private set; }
    public List<Destination> Destinations { get; private set; } = new();

    public static Cluster CreateInitialCluster(string clusterName, string address)
    {
        var initialDestination = Destination.CreateInitialDestination("destination1", address);

        var initialCluster = new Cluster(Guid.NewGuid(), new Name(clusterName), initialDestination);

        return initialCluster;
    }
}