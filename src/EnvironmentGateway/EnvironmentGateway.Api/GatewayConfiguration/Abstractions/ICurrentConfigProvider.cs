using EnvironmentGateway.Application.GatewayConfigs.GetStartConfig;
using EnvironmentGateway.Domain.Abstractions;

namespace EnvironmentGateway.Api.GatewayConfiguration.Abstractions;

public interface ICurrentConfigProvider
{
    Task<Result<StartConfigResponse>> GetCurrentConfig();
    Task<Result> CreateCurrentConfig();
}