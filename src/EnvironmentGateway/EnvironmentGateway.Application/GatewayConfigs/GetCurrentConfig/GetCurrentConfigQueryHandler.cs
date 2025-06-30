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
        CurrentConfig? currentConfig = await context
            .Database
            .SqlQuery<CurrentConfig>($"""
                SELECT 
                    gc.id AS id,
                    gc.name_value AS gateway_config_name,
                    gc.is_current_config AS is_current_config
                FROM gateway_configs gc
                WHERE gc.is_current_config = true
                """)
            .FirstOrDefaultAsync(cancellationToken);
        
        if (currentConfig is null)
        {
            return Result.Failure<CurrentConfigResponse>(Error.NullValue);
        }
        
        var currentRoutes = await context
            .Database
            .SqlQuery<CurrentRoutes>($"""
                SELECT 
                    r.id AS id,
                    r.route_name_value AS route_name,
                    r.cluster_name_value AS cluster_name,
                    rm.path_value AS match_path
                FROM routes r
                LEFT JOIN route_matches rm ON r.id = rm.route_id
                WHERE r.gateway_config_id = {currentConfig?.Id}
                """)
            .ToListAsync(cancellationToken);
        
        var currentClusters = await context
            .Database
            .SqlQuery<CurrentClusters>($"""
                SELECT 
                    c.id AS id,
                    c.cluster_name_value AS cluster_name
                FROM clusters c
                WHERE c.gateway_config_id = {currentConfig?.Id}
                """)
            .ToListAsync(cancellationToken);
        
        var currentDestinations = await context
            .Database
            .SqlQuery<CurrentDestinations>($"""
                SELECT 
                    d.id AS id,
                    d.destination_name AS destination_name,
                    d.address AS Address
                FROM destinations d
                WHERE d.cluster_id = {currentClusters[0]?.Id}
                """)
            .ToListAsync(cancellationToken);

        var response = new CurrentConfigResponse()
        {
            Id = currentConfig.Id,
            Name = currentConfig.GatewayConfigName,
            IsCurrentConfig = currentConfig.IsCurrentConfig,
            Routes = new List<RouteResponse>(),
            Clusters = new List<ClusterResponse>()
        }; 
        
        currentRoutes.ForEach(route =>
        {
            response.Routes.Add(new RouteResponse()
            {
                Id = route.Id,
                RouteName = route.RouteName,
                ClusterName = route.ClusterName,
                Match = new RouteMatchResponse(){ Path = route.MatchPath }
                
            });
        });
        
        currentClusters.ForEach(cluster =>
        {
            var destinations = new Dictionary<string, DestinationResponse>();
            currentDestinations.ForEach(destination =>
            {
                var key = destination.DestinationName;
                var value = new DestinationResponse()
                {
                    Id = destination.Id,
                    DestinationName = destination.DestinationName,
                    Address = destination.Address
                };
                destinations.Add(key, value);
            });
            response.Clusters.Add(new ClusterResponse()
            {
                Id = cluster.Id,
                ClusterName = cluster.ClusterName,
                Destinations = destinations
            });
        });


        return response;
    }

    private sealed record CurrentConfig(
        Guid Id,
        string GatewayConfigName,
        bool IsCurrentConfig);

    private sealed record CurrentRoutes(
        Guid Id,
        string RouteName,
        string ClusterName,
        string MatchPath);
    
    private sealed record CurrentClusters(
        Guid Id,
        string ClusterName);

    private sealed record CurrentDestinations(
        Guid Id,
        string DestinationName,
        string Address);
}
