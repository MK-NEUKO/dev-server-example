namespace EnvironmentGateway.Api.GatewayConfiguration.Abstractions;

public interface IRuntimeConfigurator
{
    void ChangeDestinationAddress(string address);

    Task InitializeGateway();
}