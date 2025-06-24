using EnvironmentGateway.Application.Abstractions.Messaging;

namespace EnvironmentGateway.Application.Destinations.UpdateDestination;

public sealed record UpdateDestinationCommand(
    Guid ClusterId,
    Guid DestinationId,
    string Address)
    : ICommand;
