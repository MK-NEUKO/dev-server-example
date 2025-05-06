namespace EnvironmentGateway.Api.Endpoints.UpdateConfig;

public sealed record UpdateDestinationRequest(
    Guid Id,
    string Address);