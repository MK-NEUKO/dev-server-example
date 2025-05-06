using EnvironmentGateway.Domain.Destinations;

namespace EnvironmentGateway.Infrastructure.Repositories;

internal sealed class DestinationRepository(
    EnvironmentGatewayDbContext dbContext)
    : Repository<Destination>(dbContext), IDestinationRepository
{

}