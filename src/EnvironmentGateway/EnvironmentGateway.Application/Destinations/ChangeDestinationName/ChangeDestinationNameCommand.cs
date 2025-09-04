using EnvironmentGateway.Application.Abstractions.Messaging;

namespace EnvironmentGateway.Application.Destinations.ChangeDestinationName;

public sealed record ChangeDestinationNameCommand(
    Guid ClusterId,
    Guid DestinationId,
    string DestinationName)
    : ICommand;
