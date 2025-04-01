namespace EnvironmentGateway.Application.GatewayConfigs.GetStartConfig;

public sealed record ClusterResponse
{
    public string ClusterName { get; init; }
    public List<DestinationResponse> Destinations { get; init; }
}