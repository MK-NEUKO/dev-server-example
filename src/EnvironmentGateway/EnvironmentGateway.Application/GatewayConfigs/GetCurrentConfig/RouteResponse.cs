namespace EnvironmentGateway.Application.GatewayConfigs.GetCurrentConfig;

public sealed record RouteResponse
{
    public required Guid Id { get; init; }
    public required string RouteName { get; init; }
    public required string ClusterName { get; init; }
    public required RouteMatchResponse Match { get; init; }
}
