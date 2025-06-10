namespace EnvironmentGateway.Application.GatewayConfigs.GetCurrentConfig;

public sealed record DestinationResponse
{
    public required Guid Id { get; init; }
    public required string DestinationName { get; init; }
    public required string Address { get; init; }
}