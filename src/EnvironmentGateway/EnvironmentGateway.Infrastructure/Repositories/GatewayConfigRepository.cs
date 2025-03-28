using EnvironmentGateway.Domain.GatewayConfigs;

namespace EnvironmentGateway.Infrastructure.Repositories;

internal sealed class GatewayConfigRepository : Repository<GatewayConfig>, IGatewayConfigRepository
{
    public GatewayConfigRepository(EnvironmentGatewayDbContext dbContext) 
        : base(dbContext)
    {
    }
}