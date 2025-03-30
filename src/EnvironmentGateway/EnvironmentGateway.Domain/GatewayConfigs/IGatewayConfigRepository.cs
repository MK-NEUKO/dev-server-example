namespace EnvironmentGateway.Domain.GatewayConfigs;

public interface IGatewayConfigRepository
{
    Task<GatewayConfigs.GatewayConfig?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<bool> IsCurrentConfigExists(CancellationToken cancellationToken = default);

    void Add(GatewayConfigs.GatewayConfig gatewayConfig);
}