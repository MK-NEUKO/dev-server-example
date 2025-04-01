namespace EnvironmentGateway.Application.GatewayConfigs.GetStartConfig;

public sealed class StartConfigResponse()
{
    public Guid Id { get; init; } 
    public string Name { get; init; } 
    public bool IsCurrentConfig { get; init; } 
    public List<RouteResponse> Routes { get; init; } 
    public List<ClusterResponse> Clusters { get; init; } 
}
