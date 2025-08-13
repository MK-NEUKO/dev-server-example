namespace EnvironmentGateway.Application.GatewayConfigs.GetCurrentConfig;

public sealed record RouteTransformsResponse
{
    public required Guid Id { get; init; }
    public required List<Dictionary<string, string>> Transforms { get; init; }
}
