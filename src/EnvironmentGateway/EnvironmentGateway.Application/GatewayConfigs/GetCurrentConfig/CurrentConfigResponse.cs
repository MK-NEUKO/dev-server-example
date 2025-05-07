namespace EnvironmentGateway.Application.GatewayConfigs.GetCurrentConfig;

public sealed class CurrentConfigResponse
{
    public required Guid Id { get; init; } 
    public required string Name { get; init; } 
    public required bool IsCurrentConfig { get; init; } 
    public required List<RouteResponse> Routes { get; init; } 
    public required List<ClusterResponse> Clusters { get; init; } 
}
