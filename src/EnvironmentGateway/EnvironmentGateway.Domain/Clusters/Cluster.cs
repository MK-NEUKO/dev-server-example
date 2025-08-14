using EnvironmentGateway.Domain.Abstractions;
using EnvironmentGateway.Domain.Destinations;
using EnvironmentGateway.Domain.GatewayConfigs;
using EnvironmentGateway.Domain.Shared;

namespace EnvironmentGateway.Domain.Clusters;

public sealed class Cluster : Entity
{
    private Cluster(Guid id, Name clusterName)
        : base(id)
    {
        ClusterName = clusterName;
    }

    private Cluster()
    {
    }

    public Guid GatewayConfigId { get; init; }
    public GatewayConfig GatewayConfig { get; init; } = null!;
    public Name ClusterName { get; init; } = new Name("cluster1");
    public List<Destination> Destinations { get; } = [];

    public static Cluster Create(
        string clusterName, 
        string destinationName, 
        string destinationAddress)
    {
        ArgumentNullException.ThrowIfNull(clusterName);
        ArgumentNullException.ThrowIfNull(destinationName);
        ArgumentNullException.ThrowIfNull(destinationAddress);
        
        var destination = Destination.Create(destinationName, destinationAddress);
        var cluster = new Cluster(Guid.NewGuid(), new Name(clusterName));
        cluster.AddDestination(destination);

        return cluster;
    }
    
    public void AddDestination(Destination destination)
    {
        ArgumentNullException.ThrowIfNull(destination);
        
        Destinations.Add(destination);
    }
}
