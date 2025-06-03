namespace EnvironmentGateway.Api.Endpoints.UpdateConfig.UpdateDestination;

public sealed record UpdateDestinationRequest(
    Guid Id,
    string Address);