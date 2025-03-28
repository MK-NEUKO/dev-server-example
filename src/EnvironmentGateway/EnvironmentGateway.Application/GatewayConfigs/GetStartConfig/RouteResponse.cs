namespace EnvironmentGateway.Application.GatewayConfigs.GetStartConfig;

public sealed record RouteResponse(
    string RouteName,
    string ClusterName,
    string MatchPath);