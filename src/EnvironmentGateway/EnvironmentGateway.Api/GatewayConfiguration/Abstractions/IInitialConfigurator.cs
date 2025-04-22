namespace EnvironmentGateway.Api.GatewayConfiguration.Abstractions;

internal interface IInitialConfigurator
{
    Task<InitialConfiguration> GetInitialConfigurationAsync(CancellationToken cancellationToken = default);
}