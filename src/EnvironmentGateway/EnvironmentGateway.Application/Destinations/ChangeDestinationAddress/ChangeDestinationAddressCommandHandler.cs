using EnvironmentGateway.Application.Abstractions.Messaging;
using EnvironmentGateway.Domain.Abstractions;
using EnvironmentGateway.Domain.Clusters.Destinations;

namespace EnvironmentGateway.Application.Destinations.ChangeDestinationAddress;

internal sealed class ChangeDestinationAddressCommandHandler(
    IDestinationRepository destinationRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<ChangeDestinationAddressCommand>
{
    public async Task<Result> Handle(ChangeDestinationAddressCommand request, CancellationToken cancellationToken)
    {
        var destination = await destinationRepository.GetByIdAsync(request.ClusterId, request.DestinationId, cancellationToken);

        if (destination is null)
        {
            return Result.Failure(DestinationErrors.NotFound);
        }

        destination.ChangeAddress(request.Address);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
