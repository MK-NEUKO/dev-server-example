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
    public List<TransformsItem> TransformsItems { get; private set; } = new();

    public static RouteTransforms Create(string key, string value)
    {
        ArgumentNullException.ThrowIfNull(key);
        ArgumentNullException.ThrowIfNull(value);
        
        var newRoutTransforms = new RouteTransforms(Guid.NewGuid());
        newRoutTransforms.AddTransformsItem(key, value);
        
        return newRoutTransforms;
    }
    
    public void AddTransformsItem(string key, string value)
    {
        ArgumentNullException.ThrowIfNull(key);
        ArgumentNullException.ThrowIfNull(value);
        
        var transform = new TransformsItem(key, value);
        
        TransformsItems.Add(transform);
    }
}
