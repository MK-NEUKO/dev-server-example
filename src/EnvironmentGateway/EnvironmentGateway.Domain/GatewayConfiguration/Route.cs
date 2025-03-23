using EnvironmentGateway.Domain.Abstractions;

namespace EnvironmentGateway.Domain.GatewayConfiguration;

public sealed class Route : Entity
{
    public Route(Guid id)
        : base(id)
    {
    }

    public RouteId RouteId { get; private set; }
    public ClusterId ClusterId { get; private set; }
    public RouteMatch Match { get; private set; }
}