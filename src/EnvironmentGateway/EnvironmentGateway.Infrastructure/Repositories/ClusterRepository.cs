using EnvironmentGateway.Domain.Clusters;

namespace EnvironmentGateway.Infrastructure.Repositories;

internal sealed class ClusterRepository(
    EnvironmentGatewayDbContext dbContext)
    : Repository<Cluster>(dbContext), IClusterRepository
{
    
}
