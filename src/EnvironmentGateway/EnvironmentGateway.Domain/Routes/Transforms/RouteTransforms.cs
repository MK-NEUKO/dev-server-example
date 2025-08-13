using EnvironmentGateway.Domain.Abstractions;

namespace EnvironmentGateway.Domain.Routes.Transforms;

public sealed class RouteTransforms : Entity
{
    private RouteTransforms(Guid id)
        : base(id)
    {
    }
    private RouteTransforms()
    {
    }
    
    public Guid RouteId { get; init; }
    public Route Route { get; init; } = null!;
    public List<Transform> Transforms { get; private set; } = new();

    public static RouteTransforms Create()
    {
        return new RouteTransforms(Guid.NewGuid());
    }
    
    public void AddTransform(string key, string value)
    {
        var transform = new Transform(key, value);
        Transforms.Add(transform);
    }
}
