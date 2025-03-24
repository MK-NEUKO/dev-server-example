using EnvironmentGateway.Domain.Abstractions;

namespace EnvironmentGateway.Domain.GatewayConfig;

public sealed class Route : Entity
{
    public Route(Guid id)
        : base(id)
    {
    }

    public Guid GatewayConfigId { get; private set; }
    public RouteId RouteId { get; private set; }
    public ClusterId ClusterId { get; private set; }
    public RouteMatch Match { get; private set; }
}