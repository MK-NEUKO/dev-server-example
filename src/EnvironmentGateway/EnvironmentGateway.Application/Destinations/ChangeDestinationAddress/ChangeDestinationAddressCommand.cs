using EnvironmentGateway.Application.Abstractions.Messaging;

namespace EnvironmentGateway.Application.Destinations.UpdateDestination;

public sealed record ChangeDestinationAddressCommand(
    Guid ClusterId,
    Guid DestinationId,
    string Address)
    : ICommand;
