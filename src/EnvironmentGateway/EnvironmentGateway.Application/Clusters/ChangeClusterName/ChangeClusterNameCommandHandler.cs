using EnvironmentGateway.Application.Abstractions.Messaging;
using EnvironmentGateway.Domain.Abstractions;
using EnvironmentGateway.Domain.Clusters;

namespace EnvironmentGateway.Application.Clusters.ChangeClusterName;

internal sealed class ChangeClusterNameCommandHandler(
    IClusterRepository clusterReppository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<ChangeClusterNameCommand>
{
    public async Task<Result> Handle(
        ChangeClusterNameCommand command, 
        CancellationToken cancellationToken)
    {
        var cluster = await clusterReppository
            .GetByIdAsync(command.ClusterId, cancellationToken);

        if (cluster is null)
        {
            return Result.Failure(ClusterErrors.NotFound);
        }

        cluster.ChangeName(command.ClusterName);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }
}
