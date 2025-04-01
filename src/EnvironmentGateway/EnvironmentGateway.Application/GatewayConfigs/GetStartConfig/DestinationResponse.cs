namespace EnvironmentGateway.Application.GatewayConfigs.GetStartConfig;

public sealed record DestinationResponse
{
    public string DestinationName { get; init; }
    public string Address { get; init; }
}