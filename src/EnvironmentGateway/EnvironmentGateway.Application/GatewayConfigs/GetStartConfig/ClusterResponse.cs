namespace EnvironmentGateway.Application.GatewayConfigs.GetStartConfig;

public sealed record ClusterResponse(
    string ClusterName,
    List<DestinationDto> Destinatons);