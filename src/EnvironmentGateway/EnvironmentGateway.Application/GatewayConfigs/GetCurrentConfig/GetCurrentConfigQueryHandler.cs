using EnvironmentGateway.Application.Abstractions.Data;
using EnvironmentGateway.Application.Abstractions.Messaging;
using EnvironmentGateway.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace EnvironmentGateway.Application.GatewayConfigs.GetCurrentConfig;

internal sealed class GetCurrentConfigQueryHandler(IEnvironmentGatewayDbContext context)
    : IQueryHandler<GetCurrentConfigQuery, CurrentConfigResponse>
{
    public async Task<Result<CurrentConfigResponse>> Handle(
        GetCurrentConfigQuery request,
        CancellationToken cancellationToken)
    {
        var gatewayConfigSummery = await context
            .Database
            .SqlQuery<GatewayConfigSummery>($"""
                SELECT 
                    gc.id AS gateway_config_id,
                    gc.name_value AS gateway_config_name,
                    gc.is_current_config AS is_current_config,
                    r.cluster_name_value AS route_cluster_name,
                    r.route_name_value AS route_name,
                    rm.path_value AS match_path,
                    c.cluster_name_value AS cluster_name,
                    d.id AS destination_id,
                    d.destination_name_value AS destination_name,
                    d.address_value AS destination_address
                    
                FROM gateway_configs gc
                LEFT JOIN routes r ON gc.id = r.gateway_config_id
                LEFT JOIN route_matches rm ON r.id = rm.route_id
                LEFT JOIN clusters c ON gc.id = c.gateway_config_id
                LEFT JOIN destinations d ON c.id = d.cluster_id
                WHERE gc.is_current_config = true
                """)
            .FirstOrDefaultAsync(cancellationToken);

        if (gatewayConfigSummery is null)
        {
            return Result.Failure<CurrentConfigResponse>(Error.NullValue);
        }

        var response = new CurrentConfigResponse()
        {
            Id = gatewayConfigSummery.GatewayConfigId,
            Name = gatewayConfigSummery.GatewayConfigName,
            IsCurrentConfig = gatewayConfigSummery.IsCurrentConfig,
            Routes =
            [
                new RouteResponse()
                {
                    RouteName = gatewayConfigSummery.RouteName,
                    ClusterName = gatewayConfigSummery.RouteClusterName,
                    Match = new RouteMatchResponse()
                    {
                        Path = gatewayConfigSummery.MatchPath
                    }
                }
            ],
            Clusters =
            [
                new ClusterResponse()
                {
                    ClusterName = gatewayConfigSummery.ClusterName,
                    Destinations =
                    [
                        new DestinationResponse()
                        {
                            Id = gatewayConfigSummery.DestinationId,
                            DestinationName = gatewayConfigSummery.DestinationName,
                            Address = gatewayConfigSummery.DestinationAddress
                        }
                    ]
                }
            ]
        };


        return response;
    }

    private sealed record GatewayConfigSummery(
        Guid GatewayConfigId,
        string GatewayConfigName,
        bool IsCurrentConfig,
        string RouteClusterName,
        string RouteName,
        string MatchPath,
        string ClusterName,
        Guid DestinationId,
        string DestinationName,
        string DestinationAddress);
}