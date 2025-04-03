using EnvironmentGateway.Application.Abstractions.Messaging;

namespace EnvironmentGateway.Application.GatewayConfigs.GetStartConfig;

public sealed record GetStartConfigQuery(bool isCurrentConfig) : IQuery<StartConfigResponse>;