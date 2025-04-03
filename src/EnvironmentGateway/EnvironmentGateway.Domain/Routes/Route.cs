using EnvironmentGateway.Domain.Abstractions;
using EnvironmentGateway.Domain.GatewayConfigs;
using EnvironmentGateway.Domain.RouteMatches;
using EnvironmentGateway.Domain.Shared;

namespace EnvironmentGateway.Domain.Routes;

public sealed class Route : Entity
{
    private Route(Guid id, Name routeName, Name clusterName, RouteMatch match)
        : base(id)
    {
        RouteName = routeName;
        ClusterName = clusterName;
        Match = match;
    }

    private Route()
    {
    }

    public Guid GatewayConfigId { get; init; }
    public GatewayConfig GatewayConfig { get; init; } = null!;
    public Name RouteName { get; init; } = new Name("route1");
    public Name ClusterName { get; init; } = new Name("cluster1");
    public RouteMatch? Match { get; private set; }

    public static Route CreateInitialRoute(string routeName, string clusterName, string matchPath)
    {
        var match = RouteMatch.CreateInitialRouteMatch(matchPath);

        var route = new Route(Guid.NewGuid(), new Name(routeName), new Name(clusterName), match);
        
        return route;
    }
}