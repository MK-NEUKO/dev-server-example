namespace EnvironmentGateway.Api.Endpoints.Destinations.ChangeDestinationName;

public sealed record ChangeDestinationNameRequest(
    Guid ClusterId,
    Guid DestinationId,
    string DestinationName);
