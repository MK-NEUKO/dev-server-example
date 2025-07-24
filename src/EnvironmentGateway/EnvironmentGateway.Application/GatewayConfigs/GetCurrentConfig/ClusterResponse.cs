namespace EnvironmentGateway.Application.GatewayConfigs.GetCurrentConfig;

public sealed record ClusterResponse
{
    public required Guid Id { get; init; }
    public required string ClusterName { get; init; }
    public required Dictionary<string, DestinationResponse> Destinations { get; init; }
}
