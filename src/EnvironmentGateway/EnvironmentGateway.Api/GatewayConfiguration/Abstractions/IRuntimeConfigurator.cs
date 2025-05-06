namespace EnvironmentGateway.Api.GatewayConfiguration.Abstractions;

public interface IRuntimeConfigurator
{
    Task InitializeGateway();
    Task UpdateConfig();
}