using EnvironmentGateway.Domain.Abstractions;
using EnvironmentGateway.Domain.GatewayConfigs;
using EnvironmentGateway.Domain.Routes.Match;
using EnvironmentGateway.Domain.Routes.Transforms;
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
    public Name RouteName { get; init; } = new Name("");
    public Name ClusterName { get; init; } = new Name("");
    public RouteMatch? Match { get; private set; }
    public RouteTransforms? Transforms { get; private set; }

    public static Route Create(string routeName, string clusterName, string matchPath)
    {
        ArgumentNullException.ThrowIfNull(routeName);
        ArgumentNullException.ThrowIfNull(clusterName);
        ArgumentNullException.ThrowIfNull(matchPath);
        
        var match = RouteMatch.Create(matchPath);
        var transforms = RouteTransforms.Create(TransformKeys.PathPattern, ConfigDefaultParams.TransformValue);
        var route = new Route(Guid.NewGuid(), new Name(routeName), new Name(clusterName), match);
        route.AddTransforms(transforms);
        
        return route;
    }

    public void AddTransforms(RouteTransforms transforms)
    {
        ArgumentNullException.ThrowIfNull(transforms);
        
        Transforms = transforms;
    }
}
