namespace EnvironmentGateway.Api.GatewayConfiguration.Abstractions;

public interface IRuntimeConfigurator
{
    Task UpdateDefaultProxyConfig();
    Task UpdateYarpProxy();
}