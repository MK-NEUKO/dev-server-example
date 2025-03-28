namespace EnvironmentGateway.Domain.GatewayConfigs;

public interface IGatewayConfigRepository
{
    Task<GatewayConfigs.GatewayConfig?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    void Add(GatewayConfigs.GatewayConfig gatewayConfig);
}