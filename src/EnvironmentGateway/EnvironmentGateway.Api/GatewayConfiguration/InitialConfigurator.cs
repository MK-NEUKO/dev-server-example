using EnvironmentGateway.Application.GatewayConfigs.CreateInitialConfig;
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
        // Query a database to get the initial configuration.

        // If the database has no configuration or is not available,
        // get a default configuration from domain.
        var command = new CreateInitialConfigCommand("initialConfiguration");
        Result<Guid> result = await _sender.Send(command, CancellationToken.None);
        

        // Apply configuration to gateway.


        var initialConfig = new InitialConfiguration(GetRoutes(), GetClusters());

        return initialConfig;
    }

    private RouteConfig[] GetRoutes()
    {
        return
        [
            new RouteConfig()
            {
                RouteId = "route" + Random.Shared.Next(), // Forces a new route id each time GetRoutes is called.
                ClusterId = "cluster1",
                Match = new RouteMatch
                {
                    // Path or Hosts are required for each route. This catch-all pattern matches all request paths.
                    Path = "{**catch-all}"
                }
            }
        ];
    }

    private ClusterConfig[] GetClusters()
    {
        return
        [
            new ClusterConfig()
            {
                ClusterId = "cluster1",
                Destinations = new Dictionary<string, DestinationConfig>(StringComparer.OrdinalIgnoreCase)
                {
                    { "destination1", new DestinationConfig() { Address = "https://neuko-know-how.com" } },
                }
            }
        ];
    }
}