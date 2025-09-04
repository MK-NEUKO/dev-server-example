using EnvironmentGateway.Application.Abstractions.Messaging;
using EnvironmentGateway.Domain.Abstractions;
using EnvironmentGateway.Domain.Clusters.Destinations;

namespace EnvironmentGateway.Application.Destinations.ChangeDestinationName;

internal sealed class ChangeDestinationNameCommandHandler(
    IDestinationRepository destinationRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<ChangeDestinationNameCommand>
{
    public async Task<Result> Handle(
        ChangeDestinationNameCommand command, 
        CancellationToken cancellationToken)
    {
        var destination = await destinationRepository
            .GetByIdAsync(command.ClusterId, command.DestinationId, cancellationToken);
        
        if (destination is null)
        {
            return Result.Failure(DestinationErrors.NotFound);
        }

        destination.ChangeName(command.DestinationName);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
