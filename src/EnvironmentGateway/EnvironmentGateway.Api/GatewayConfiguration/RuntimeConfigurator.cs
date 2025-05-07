using EnvironmentGateway.Api.GatewayConfiguration.Abstractions;
using EnvironmentGateway.Application.GatewayConfigs.CreateInitialConfig;
using EnvironmentGateway.Application.GatewayConfigs.GetStartConfig;
using EnvironmentGateway.Domain.Abstractions;
using EnvironmentGateway.Domain.GatewayConfigs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Yarp.ReverseProxy.Configuration;

namespace EnvironmentGateway.Api.GatewayConfiguration;

internal sealed class RuntimeConfigurator(
    InMemoryConfigProvider inMemoryConfigProvider,
    ICurrentConfigProvider currentConfigProvider,
    ILogger<RuntimeConfigurator> logger) 
    : IRuntimeConfigurator
{
    public async Task<Result> UpdateDefaultProxyConfig()
    {
        var currentConfigResult = await currentConfigProvider.GetCurrentConfig();

        if (currentConfigResult.IsFailure)
        {
            var createConfigResult = await currentConfigProvider.CreateCurrentConfig();

            if (createConfigResult.IsFailure)
            {
                logger.LogError("Error in UpdateDefaultProxyConfig, unable to create current configuration; {Error}", createConfigResult.Error);
                return Result.Failure(GatewayErrors.UpdateDefaultProxyConfigFailed);
            }

            currentConfigResult = await currentConfigProvider.GetCurrentConfig();
        }

        var proxyConfig = ProxyConfigMapper.Map(currentConfigResult.Value);

        UpdateConfig(proxyConfig);

        return Result.Success("Success: default proxy config ist successfully updated.");
    }

    public async Task UpdateProxyConfig()
    {
        var result = await currentConfigProvider.GetCurrentConfig();

        if (result.IsSuccess)
        {
            var proxyConfig = ProxyConfigMapper.Map(result.Value);
            UpdateConfig(proxyConfig);
        }
    }

    private void UpdateConfig(ProxyConfig config)
    {
        inMemoryConfigProvider.Update(config.Routes, config.Clusters);
    }
}