using EnvironmentGateway.Api.GatewayConfiguration;

namespace EnvironmentGatewayApi.GatewayConfiguration.Abstractions;

internal interface IInitialConfigurator
{
    Task<InitialConfiguration> GetInitialConfigurationAsync(CancellationToken cancellationToken = default);
}