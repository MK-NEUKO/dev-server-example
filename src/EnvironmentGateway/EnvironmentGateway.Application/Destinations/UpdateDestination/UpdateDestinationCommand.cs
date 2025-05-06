using EnvironmentGateway.Application.Abstractions.Messaging;

namespace EnvironmentGateway.Application.Destinations.UpdateDestination;

public sealed record UpdateDestinationCommand(
    Guid Id,
    string Address)
    : ICommand;