namespace EnvironmentGateway.Api.Endpoints.ChangeConfig;

public sealed record ChangeDestinationRequest(
    Guid Id,
    string Address);