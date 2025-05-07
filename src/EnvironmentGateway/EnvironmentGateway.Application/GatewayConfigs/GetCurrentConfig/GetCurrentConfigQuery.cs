using EnvironmentGateway.Application.Abstractions.Messaging;

namespace EnvironmentGateway.Application.GatewayConfigs.GetCurrentConfig;

public sealed record GetCurrentConfigQuery() : IQuery<CurrentConfigResponse>;