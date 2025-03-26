using EnvironmentGateway.Domain.Abstractions;

namespace EnvironmentGateway.Domain.GatewayConfig.Cluster;

public sealed class Destination : Entity
{
    public Destination(Guid id)
        : base(id)
    {
    }

    public Guid ClusterId { get; private set; }
    public Name DestinationName { get; private set; }
    public Url Address { get; private set; }
}