using EnvironmentGateway.Domain.Abstractions;
using EnvironmentGateway.Domain.GatewayConfigs;
using EnvironmentGateway.Domain.Routes.Match;
using EnvironmentGateway.Domain.Routes.Transforms;
using EnvironmentGateway.Domain.Shared;

namespace EnvironmentGateway.Domain.Routes;

public sealed class Route : Entity
{
    private Route(Guid id, Name routeName, Name clusterName, RouteMatch match, RouteTransforms transforms)
        : base(id)
    {
        RouteName = routeName;
        ClusterName = clusterName;
        Match = match;
        Transforms = transforms;
    }

    private Route()
    {
    }

    public Guid GatewayConfigId { get; init; }
    public GatewayConfig GatewayConfig { get; init; } = null!;
    public Name RouteName { get; init; } = new Name("route1");
    public Name ClusterName { get; init; } = new Name("cluster1");
    public RouteMatch? Match { get; private set; }
    public RouteTransforms? Transforms { get; private set; }

    public static Route Create(string routeName, string clusterName, string matchPath)
    {
        var match = RouteMatch.Create(matchPath);
        
        var transforms = RouteTransforms.Create(TransformsKey.PathPattern, "/prefix");
        transforms.AddTransformsItem("PathRemovePrefix", "/");

        var route = new Route(Guid.NewGuid(), new Name(routeName), new Name(clusterName), match, transforms);
        
        return route;
    }
}
