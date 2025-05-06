using EnvironmentGateway.Domain.Abstractions;

namespace EnvironmentGateway.Domain.Destinations;

public static class DestinationErrors
{
    public static readonly Error NotFound = new(
        "Destination.NotFound",
        "The destination with the specified identifier was not found");
}