namespace EnvironmentGateway.Application.GatewayConfigs.GetCurrentConfig;

public sealed record RouteMatchResponse
{
    public required string Path { get; init; }
}