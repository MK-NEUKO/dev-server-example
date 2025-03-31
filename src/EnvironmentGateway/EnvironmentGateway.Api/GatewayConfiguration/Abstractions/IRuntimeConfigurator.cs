using EnvironmentGateway.Api.GatewayConfiguration;

namespace EnvironmentGatewayApi.GatewayConfiguration.Abstractions;

public interface IRuntimeConfigurator
{
    void ChangeDestinationAddress(string address);

    Task InitializeGateway();
}