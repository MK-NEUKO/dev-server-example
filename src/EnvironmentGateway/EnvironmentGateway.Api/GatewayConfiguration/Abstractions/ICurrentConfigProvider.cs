using EnvironmentGateway.Application.GatewayConfigs.GetCurrentConfig;
using EnvironmentGateway.Domain.Abstractions;

namespace EnvironmentGateway.Api.GatewayConfiguration.Abstractions;

public interface ICurrentConfigProvider
{
    Task<Result<CurrentConfigResponse>> GetCurrentConfig();
    Task<Result> CreateCurrentConfig();
}