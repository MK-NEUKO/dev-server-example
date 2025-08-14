using EnvironmentGateway.Application.Abstractions.Messaging;
using EnvironmentGateway.Domain.Abstractions;
using EnvironmentGateway.Domain.Clusters.Destinations;

namespace EnvironmentGateway.Application.Destinations.UpdateDestination;

internal sealed class UpdateDestinationCommandHandler(
    IDestinationRepository destinationRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateDestinationCommand>
{
    public async Task<Result> Handle(UpdateDestinationCommand request, CancellationToken cancellationToken)
    {
        var destination = await destinationRepository.GetByIdAsync(request.ClusterId, request.DestinationId, cancellationToken);

        if (destination is null)
        {
            return Result.Failure(DestinationErrors.NotFound);
        }

        destination.Update(request.Address);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
