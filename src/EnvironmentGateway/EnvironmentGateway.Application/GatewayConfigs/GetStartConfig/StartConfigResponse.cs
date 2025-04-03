namespace EnvironmentGateway.Application.GatewayConfigs.GetStartConfig;

public sealed class StartConfigResponse
{
    public required Guid Id { get; init; } 
    public required string Name { get; init; } 
    public required bool IsCurrentConfig { get; init; } 
    public required List<RouteResponse> Routes { get; init; } 
    public required List<ClusterResponse> Clusters { get; init; } 
}
