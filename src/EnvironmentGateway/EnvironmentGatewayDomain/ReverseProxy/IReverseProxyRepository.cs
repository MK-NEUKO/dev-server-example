namespace EnvironmentGatewayDomain.NewFolder;

public interface IReverseProxyRepository
{
    Task<ReverseProxy> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}