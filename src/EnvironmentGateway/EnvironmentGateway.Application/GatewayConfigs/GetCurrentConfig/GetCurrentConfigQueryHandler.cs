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
        GatewayConfigSummary? gatewayConfigSummary = await context
            .Database
            .SqlQuery<GatewayConfigSummary>($"""
                SELECT 
                    gc.id AS gateway_config_id,
                    gc.name_value AS gateway_config_name,
                    gc.is_current_config AS is_current_config,
                    r.cluster_name_value AS route_cluster_name,
                    r.route_name_value AS route_name,
                    rm.path_value AS match_path,
                    c.cluster_name_value AS cluster_name,
                    c.id AS cluster_id,
                    d.id AS destination_id,
                    d.destination_name AS destination_name,
                    d.address AS destination_address
                    
                FROM gateway_configs gc
                LEFT JOIN routes r ON gc.id = r.gateway_config_id
                LEFT JOIN route_matches rm ON r.id = rm.route_id
                LEFT JOIN clusters c ON gc.id = c.gateway_config_id
                LEFT JOIN destinations d ON c.id = d.cluster_id
                WHERE gc.is_current_config = true
                """)
            .FirstOrDefaultAsync(cancellationToken);

        if (gatewayConfigSummary is null)
        {
            return Result.Failure<CurrentConfigResponse>(Error.NullValue);
        }

        var response = new CurrentConfigResponse()
        {
            Id = gatewayConfigSummary.GatewayConfigId,
            Name = gatewayConfigSummary.GatewayConfigName,
            IsCurrentConfig = gatewayConfigSummary.IsCurrentConfig,
            Routes =
            [
                new RouteResponse()
                {
                    RouteName = gatewayConfigSummary.RouteName,
                    ClusterName = gatewayConfigSummary.RouteClusterName,
                    Match = new RouteMatchResponse()
                    {
                        Path = gatewayConfigSummary.MatchPath
                    }
                }
            ],
            Clusters =
            [
                new ClusterResponse()
                {
                    Id = gatewayConfigSummary.ClusterId,
                    ClusterName = gatewayConfigSummary.ClusterName,
                    Destinations =
                    [
                        new DestinationResponse()
                        {
                            Id = gatewayConfigSummary.DestinationId,
                            DestinationName = gatewayConfigSummary.DestinationName,
                            Address = gatewayConfigSummary.DestinationAddress
                        }
                    ]
                }
            ]
        };


        return response;
    }

    private sealed record GatewayConfigSummary(
        Guid GatewayConfigId,
        string GatewayConfigName,
        bool IsCurrentConfig,
        string RouteClusterName,
        string RouteName,
        string MatchPath,
        Guid ClusterId,
        string ClusterName,
        Guid DestinationId,
        string DestinationName,
        string DestinationAddress);
}
