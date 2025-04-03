using EnvironmentGateway.Domain.GatewayConfigs;
using Microsoft.EntityFrameworkCore;

namespace EnvironmentGateway.Infrastructure.Repositories;

internal sealed class GatewayConfigRepository : Repository<GatewayConfig>, IGatewayConfigRepository
{
    public GatewayConfigRepository(EnvironmentGatewayDbContext dbContext) 
        : base(dbContext)
    {
    }

    public async Task<bool> IsCurrentConfigExists(CancellationToken cancellationToken = default)
    {
        return await DbContext
            .Set<GatewayConfig>()
            .AnyAsync(gatewayConfig => gatewayConfig.IsCurrentConfig);
    }
}