using EnvironmentGateway.Domain.Clusters;
using EnvironmentGateway.Domain.Clusters.Destinations;
using EnvironmentGateway.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace EnvironmentGateway.Infrastructure.Repositories;

internal sealed class DestinationRepository(
    EnvironmentGatewayDbContext dbContext)
    : Repository<Destination>(dbContext), IDestinationRepository
{
    public async Task<Destination?> GetByIdAsync(Guid clusterId, Guid destinationId, CancellationToken cancellationToken = default)
    { 
        var cluster = await DbContext
            .Set<Cluster>()
            .Include(c => c.Destinations)
            .FirstOrDefaultAsync(c => c.Id == clusterId, cancellationToken);

        return cluster?.Destinations.FirstOrDefault(d => d.Id == destinationId);
    }
}
