using EnvironmentGateway.Api.GatewayConfiguration.Abstractions;
using EnvironmentGateway.Application.Abstractions.Messaging;
using EnvironmentGateway.Application.GatewayConfigs.CreateNewConfig;
using EnvironmentGateway.Application.GatewayConfigs.GetCurrentConfig;
using EnvironmentGateway.Domain.Abstractions;

namespace EnvironmentGateway.Api.GatewayConfiguration;

internal sealed class CurrentConfigProvider(
    ICommandHandler<CreateNewConfigCommand, Guid> handler,
    IQueryHandler<GetCurrentConfigQuery, CurrentConfigResponse> queryHandler,
    ILogger<CurrentConfigProvider> logger) 
    : ICurrentConfigProvider
{
    public async Task<Result<CurrentConfigResponse>> GetCurrentConfig()
    {
        var query = new GetCurrentConfigQuery();

        var result = await queryHandler.Handle(query, CancellationToken.None);

        if (!result.IsFailure) return result;

        logger.LogError("Get current configuration failed {Result}.", result.Error);
        return result;

    }

    public async Task<Result> CreateCurrentConfig()
    {
        var command = new CreateNewConfigCommand();

        var result = await handler.Handle(command, CancellationToken.None);

        if (result.IsSuccess) return result;

        logger.LogError("Create current configuration failed {Result}.", result.Error);
        return result;
    }
}