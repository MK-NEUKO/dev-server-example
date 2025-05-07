using EnvironmentGateway.Api.GatewayConfiguration.Abstractions;
using EnvironmentGateway.Application.GatewayConfigs.CreateInitialConfig;
using EnvironmentGateway.Application.GatewayConfigs.GetStartConfig;
using EnvironmentGateway.Domain.Abstractions;
using MediatR;

namespace EnvironmentGateway.Api.GatewayConfiguration;

internal sealed class CurrentConfigProvider(
    ISender sender,
    ILogger<CurrentConfigProvider> logger) 
    : ICurrentConfigProvider
{
    public async Task<Result<StartConfigResponse>> GetCurrentConfig()
    {
        var query = new GetStartConfigQuery(true);

        Result<StartConfigResponse> result = await sender.Send(query, CancellationToken.None);

        if (result.IsFailure)
        {
            logger.LogError("Current configuration loading failed {Result}.", result.Error);
            return result;
        }

        return result;
    }

    public async Task<Result> CreateCurrentConfig()
    {
        var command = new CreateInitialConfigCommand();

        var result = await sender.Send(command, CancellationToken.None);

        if (result.IsSuccess) return result;

        logger.LogInformation("Current configuration loading failed {Result}.", result.Error);
        return result;
    }
}