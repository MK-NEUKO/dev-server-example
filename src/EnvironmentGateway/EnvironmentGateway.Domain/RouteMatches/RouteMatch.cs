using EnvironmentGateway.Domain.Abstractions;
using EnvironmentGateway.Domain.Routes;

namespace EnvironmentGateway.Domain.RouteMatches;

public sealed class RouteMatch : Entity
{
    private RouteMatch(Guid id, Path path)
        : base(id)
    {
        Path = path;
    }

    private RouteMatch()
    {
    }

    public Guid RouteId { get; init; }
    public Route Route { get; init; } = null!;
    public Path Path { get; init; } = new Path("null");

    public static RouteMatch Create(string path)
    {
        var routeMatch = new RouteMatch(Guid.NewGuid(), new Path(path));
       
        return routeMatch;
    }
}
