using EnvironmentGateway.Domain.Abstractions;

namespace EnvironmentGateway.Domain.GatewayConfig.Route;

public sealed class Route : Entity
{
    public Route(Guid id)
        : base(id)
    {
    }

    public Guid GatewayConfigId { get; private set; }
    public Name RouteName { get; private set; }
    public Name ClusterName { get; private set; }
    public RouteMatch Match { get; private set; }
}