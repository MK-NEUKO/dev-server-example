namespace EnvironmentGateway.Domain.GatewayConfig;

public interface IGatewayConfigRepository
{
    Task<GatewayConfig?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    void Add(GatewayConfig gatewayConfig);
}