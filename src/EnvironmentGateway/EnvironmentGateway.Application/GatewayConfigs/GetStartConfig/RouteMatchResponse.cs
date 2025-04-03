namespace EnvironmentGateway.Application.GatewayConfigs.GetStartConfig;

public sealed record RouteMatchResponse
{
    public required string Path { get; init; }
}