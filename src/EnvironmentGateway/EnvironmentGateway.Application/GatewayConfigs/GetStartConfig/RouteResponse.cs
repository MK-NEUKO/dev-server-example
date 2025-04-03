namespace EnvironmentGateway.Application.GatewayConfigs.GetStartConfig;

public sealed record RouteResponse
{
    public required string RouteName { get; init; }
    public required string ClusterName { get; init; }
    public required RouteMatchResponse Match { get; init; }
}