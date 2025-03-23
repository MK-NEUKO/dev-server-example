using EnvironmentGateway.Domain.Abstractions;

namespace EnvironmentGateway.Domain.GatewayConfiguration;

public sealed class GatewayConfiguration : Entity
{
    public GatewayConfiguration(Guid id)
        : base(id)
    {
    }

    public Name Name { get; private set; }

    public List<Route> Routes { get; private set; } = new();

    public List<Cluster> Clusters { get; private set; } = new();
}