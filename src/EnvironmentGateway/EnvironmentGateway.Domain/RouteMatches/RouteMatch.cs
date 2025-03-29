using EnvironmentGateway.Domain.Abstractions;

namespace EnvironmentGateway.Domain.RouteMatches;

public sealed class RouteMatch : Entity
{
    public RouteMatch()
    {
    }

    public RouteMatch(Guid id, Path path)
        : base(id)
    {
        Path = path;
    }

    public Guid RouteId { get; private set; }
    public Path Path { get; private set; }

    public static RouteMatch CreateInitialRouteMatch(string path)
    {
        var routeMatch = new RouteMatch(Guid.NewGuid(), new Path(path));
       
        return routeMatch;
    }
}