namespace EnvironmentGateway.Application.GatewayConfigs.GetStartConfig;

public sealed record RouteResponse
{
    public string RouteName { get; init; }
    public string ClusterName { get; init; }
    public RouteMatchResponse Match { get; init; }
}