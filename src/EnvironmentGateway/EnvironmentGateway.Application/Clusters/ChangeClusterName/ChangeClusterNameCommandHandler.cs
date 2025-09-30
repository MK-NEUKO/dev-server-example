using EnvironmentGateway.Application.Abstractions.Messaging;
using EnvironmentGateway.Domain.Abstractions;
using EnvironmentGateway.Domain.Clusters;

namespace EnvironmentGateway.Application.Clusters.ChangeClusterName;

internal sealed class ChangeClusterNameCommandHandler(
    IClusterRepository clusterReppository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<ChangeClusterNameCommand>
{
    public Task<Result> Handle(ChangeClusterNameCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
