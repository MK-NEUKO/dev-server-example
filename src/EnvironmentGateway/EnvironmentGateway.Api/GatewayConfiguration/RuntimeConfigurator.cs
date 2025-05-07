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
    public async Task UpdateDefaultProxyConfig()
    {
        var loadConfigResult = await currentConfigProvider.LoadCurrentConfig();

        if (loadConfigResult.IsSuccess)
        {
            var proxyConfig = ProxyConfigMapper.Map(loadConfigResult.Value);
            UpdateConfig(proxyConfig);
            return;
        }

        var createConfigResult = await currentConfigProvider.CreateCurrentConfig();

        if (createConfigResult.IsSuccess)
        {
            loadConfigResult = await currentConfigProvider.LoadCurrentConfig();

            if (loadConfigResult.IsSuccess)
            {
                var proxyConfig = ProxyConfigMapper.Map(loadConfigResult.Value);
                UpdateConfig(proxyConfig);
            }
        }


    }

    public async Task UpdateYarpProxy()
    {
        var loadConfigResult = await currentConfigProvider.LoadCurrentConfig();

        if (loadConfigResult.IsSuccess)
        {
            var proxyConfig = ProxyConfigMapper.Map(loadConfigResult.Value);
            UpdateConfig(proxyConfig);
        }
    }

    private void UpdateConfig(ProxyConfig config)
    {
        inMemoryConfigProvider.Update(config.Routes, config.Clusters);
    }
}