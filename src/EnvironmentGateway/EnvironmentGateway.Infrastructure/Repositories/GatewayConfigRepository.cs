using EnvironmentGateway.Domain.GatewayConfigs;
using Microsoft.EntityFrameworkCore;

namespace EnvironmentGateway.Infrastructure.Repositories;

internal sealed class GatewayConfigRepository : Repository<GatewayConfig>, IGatewayConfigRepository
{
    public GatewayConfigRepository(EnvironmentGatewayDbContext dbContext) 
        : base(dbContext)
    {
    }

    public async Task<Guid?> GetCurrentConfigId(CancellationToken cancellationToken = default)
    {
        return await DbContext
            .Set<GatewayConfig>()
            .Where(gatewayConfig => gatewayConfig.IsCurrentConfig)
            .Select(gatewayConfig => gatewayConfig.Id)
            .FirstOrDefaultAsync(cancellationToken);
    }
}