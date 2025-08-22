namespace EnvironmentGateway.Api.Endpoints.Destinations.ChangeDestinationAddress;

public sealed record ChangeDestinationAddressRequest(
    Guid ClusterId,
    Guid DestinationId,
    string Address);
