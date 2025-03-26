using EnvironmentGateway.Domain.Abstractions;

namespace EnvironmentGateway.Domain.GatewayConfig.Cluster;

public sealed class Cluster : Entity
{
    public Cluster(Guid id)
        : base(id)
    {
    }

    public Guid GatewayConfigId { get;private set; }
    public Name ClusterName { get; private set; }
    public List<Destination> Destinations { get; private set; } = new();
}