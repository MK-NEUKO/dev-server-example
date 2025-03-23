using EnvironmentGateway.Domain.Abstractions;

namespace EnvironmentGateway.Domain.GatewayConfiguration;

public sealed class Cluster : Entity
{
    public Cluster(Guid id)
        : base(id)
    {
    }

    public ClusterId ClusterId { get; private set; }
    public List<Dictionary<string, Destination>> Destinations { get; private set; } = new();
}