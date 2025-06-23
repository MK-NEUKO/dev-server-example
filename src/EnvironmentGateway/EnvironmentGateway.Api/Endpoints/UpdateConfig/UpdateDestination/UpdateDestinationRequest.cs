namespace EnvironmentGateway.Api.Endpoints.UpdateConfig.UpdateDestination;

public sealed record UpdateDestinationRequest(
    Guid ClusterId,
    Guid DestinationId,
    string Address);
