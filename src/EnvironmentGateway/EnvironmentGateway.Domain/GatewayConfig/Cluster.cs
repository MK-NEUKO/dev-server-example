using EnvironmentGateway.Domain.Abstractions;

namespace EnvironmentGateway.Domain.GatewayConfig;

public sealed class Cluster : Entity
{
    public Cluster(Guid id)
        : base(id)
    {
    }

    public Guid GatewayConfigId { get;private set; }
    public ClusterId ClusterId { get; private set; }
    public List<Dictionary<string, Destination>> Destinations { get; private set; } = new();
}