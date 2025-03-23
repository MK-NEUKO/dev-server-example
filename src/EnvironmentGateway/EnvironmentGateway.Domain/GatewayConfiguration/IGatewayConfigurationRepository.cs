namespace EnvironmentGateway.Domain.GatewayConfiguration;

public interface IGatewayConfigurationRepository
{
    Task<GatewayConfiguration?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    void Add(GatewayConfiguration gatewayConfiguration);
}