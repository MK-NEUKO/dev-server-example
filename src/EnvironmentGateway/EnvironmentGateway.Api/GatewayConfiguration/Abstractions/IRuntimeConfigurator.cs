using EnvironmentGateway.Domain.Abstractions;

namespace EnvironmentGateway.Api.GatewayConfiguration.Abstractions;

public interface IRuntimeConfigurator
{
    Task<Result> UpdateDefaultProxyConfig();
    Task<Result> UpdateProxyConfig();
}