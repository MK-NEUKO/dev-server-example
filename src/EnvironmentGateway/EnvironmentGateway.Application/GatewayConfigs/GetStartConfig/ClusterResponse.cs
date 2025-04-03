namespace EnvironmentGateway.Application.GatewayConfigs.GetStartConfig;

public sealed record ClusterResponse
{
    public required string ClusterName { get; init; }
    public required List<DestinationResponse> Destinations { get; init; }
}