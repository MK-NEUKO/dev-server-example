﻿using EnvironmentGateway.Application.GatewayConfigs.CreateInitialConfig;
using EnvironmentGateway.Application.GatewayConfigs.GetStartConfig;
using EnvironmentGateway.Domain.Abstractions;
using MediatR;
using EnvironmentGatewayApi.GatewayConfiguration.Abstractions;
using Yarp.ReverseProxy.Configuration;

namespace EnvironmentGateway.Api.GatewayConfiguration;

internal class InitialConfigurator : IInitialConfigurator
{
    private readonly ISender _sender;

    public InitialConfigurator(ISender sender)
    {
        _sender = sender;
    }

    public async Task<InitialConfiguration> GetInitialConfigurationAsync(CancellationToken cancellationToken = default)
    {
        var command = new CreateInitialConfigCommand("initialConfiguration");

        Result<Guid> result = await _sender.Send(command, CancellationToken.None);

        if (result.IsFailure)
        {
            // TODO: handling the error through logs or exceptions
        }

        var query = new GetStartConfigQuery(true);

        Result<StartConfigResponse> response = await _sender.Send(query, CancellationToken.None);

        if (response.IsFailure)
        {
            // TODO: handling the error through logs or exceptions
        }

        return MapToInitialConfiguration(response);
    }

    private InitialConfiguration MapToInitialConfiguration(Result<StartConfigResponse> response)
    {
        var routes = new List<RouteConfig>();
        foreach (var route in response.Value.Routes)
        {
            var routeConfig = new RouteConfig()
            {
                RouteId = route.RouteName,
                ClusterId = route.ClusterName,
                Match = new RouteMatch()
                {
                    Path = route.Match.Path
                }
            };
            routes.Add(routeConfig);
        }

        var clusters = new List<ClusterConfig>();
        var clusterConter = 0;
        foreach (var cluster in response.Value.Clusters)
        {
            var clusterConfig = new ClusterConfig()
            {
                ClusterId = cluster.ClusterName,
                Destinations = new Dictionary<string, DestinationConfig>(StringComparer.OrdinalIgnoreCase)
                {
                    {
                        cluster.Destinations[clusterConter].DestinationName,
                        new DestinationConfig() { Address = cluster.Destinations[clusterConter].Address }

                    }
                }
            };
            clusterConter++;
            clusters.Add(clusterConfig);
        }

        return new InitialConfiguration(routes.ToArray(), clusters.ToArray());
    }
}