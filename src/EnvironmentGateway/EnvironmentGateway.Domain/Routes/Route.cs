using EnvironmentGateway.Domain.Abstractions;
using EnvironmentGateway.Domain.GatewayConfig;

namespace EnvironmentGateway.Domain.Routes;

public sealed class Route : Entity
{
    private Route()
    {
    }

    private Route(Guid id, Name routeName, Name clusterName, RouteMatch match)
        : base(id)
    {
        RouteName = routeName;
        ClusterName = clusterName;
        Match = match;
    }

    public Guid GatewayConfigId { get; private set; }
    public Name RouteName { get; private set; }
    public Name ClusterName { get; private set; }
    public RouteMatch Match { get; private set; }

    public static Route CreateInitialRoute(string routeName, string clusterName, string matchPath)
    {
        var match = new RouteMatch(matchPath);

        var route = new Route(Guid.NewGuid(), new Name(routeName), new Name(clusterName), match);
        
        return route;
    }
}