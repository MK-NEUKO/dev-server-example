using EnvironmentGateway.Application.Abstractions.Messaging;
using EnvironmentGateway.Domain.Abstractions;
using EnvironmentGateway.Domain.Destinations;
using EnvironmentGateway.Domain.GatewayConfigs;

namespace EnvironmentGateway.Application.Destinations.UpdateDestination;

internal sealed class UpdateDestinationCommandHandler(
    IDestinationRepository destinationRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateDestinationCommand>
{
    public async Task<Result> Handle(UpdateDestinationCommand request, CancellationToken cancellationToken)
    {
        var destination = await destinationRepository.GetByIdAsync(request.Id, cancellationToken);

        if (destination is null)
        {
            return Result.Failure(Error.NullValue);
        }

        destination.Update(request.Address);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}