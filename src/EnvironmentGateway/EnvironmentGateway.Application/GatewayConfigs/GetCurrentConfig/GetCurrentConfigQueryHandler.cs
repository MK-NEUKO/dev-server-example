using EnvironmentGateway.Application.Abstractions.Data;
using EnvironmentGateway.Application.Abstractions.Messaging;
using EnvironmentGateway.Domain.Abstractions;
using EnvironmentGateway.Domain.GatewayConfigs;
using EnvironmentGateway.Domain.Routes;
using EnvironmentGateway.Domain.Routes.Transforms;
using Microsoft.EntityFrameworkCore;

namespace EnvironmentGateway.Application.GatewayConfigs.GetCurrentConfig;

internal sealed class GetCurrentConfigQueryHandler(IEnvironmentGatewayDbContext context)
    : IQueryHandler<GetCurrentConfigQuery, CurrentConfigResponse>
{
    public async Task<Result<CurrentConfigResponse>> Handle(
        GetCurrentConfigQuery request,
        CancellationToken cancellationToken)
    {
        var isCurrentConfig = await context
            .GatewayConfigs
            .AnyAsync(config => config.IsCurrentConfig, cancellationToken);
        
        if (!isCurrentConfig)
        {
            return Result.Failure<CurrentConfigResponse>(GatewayConfigErrors.CurrentConfigNotFound);
        }

        CurrentConfigBaseData currentConfigBaseData = await GetCurrentConfigBaseData(cancellationToken);
        
        var currentRoutes = await context
            .Routes
            .Where(route => route.GatewayConfigId == currentConfigBaseData.Id)
            .Select(route => new CurrentRoute(
                    route.Id,
                    route.RouteName.Value,
                    route.ClusterName.Value,
                    route.Match.Path.Value,
                    new Transforms(
                        route.Transforms.Id,
                        route.Transforms.Transforms
                            .Select(item => new Dictionary<string, string>{{item.Key, item.Value}})
                            .ToList()
                        )
            ))
            .ToListAsync(cancellationToken);

        
        List<CurrentClusters> currentClusters = await context
            .Database
            .SqlQuery<CurrentClusters>($"""
                SELECT 
                    c.id AS id,
                    c.cluster_name_value AS cluster_name
                FROM clusters c
                WHERE c.gateway_config_id = {currentConfigBaseData?.Id}
                """)
            .ToListAsync(cancellationToken);

        var currentDestinations = new List<List<Destination>>();
        foreach (var cluster in currentClusters)
        {
            var destinations = await context
                .Database
                .SqlQuery<Destination>($"""
                    SELECT 
                        d.id AS id,
                        d.destination_name AS destination_name,
                        d.address AS Address
                    FROM destinations d
                    WHERE d.cluster_id = {cluster.Id}
                    """)
                .ToListAsync(cancellationToken);
            currentDestinations.Add(destinations);
        }

        var response = new CurrentConfigResponse()
        {
            Id = currentConfigBaseData.Id,
            Name = currentConfigBaseData.GatewayConfigName,
            IsCurrentConfig = currentConfigBaseData.IsCurrentConfig,
            Routes = new List<RouteResponse>(),
            Clusters = new List<ClusterResponse>()
        };
        
        currentRoutes.ForEach(route =>
        {
            var transforms = new RouteTransformsResponse()
            {
                Id = route.Transforms.Id, 
                Transforms = route.Transforms.TransformItems
            };
            response.Routes.Add(new RouteResponse()
            {
                Id = route.Id,
                RouteName = route.RouteName,
                ClusterName = route.ClusterName,
                Match = new RouteMatchResponse(){ Path = route.MatchPath },
                Transforms = transforms
            });
        });

        var clusterIndex = 0;
        currentClusters.ForEach(cluster =>
        {
            var destinations = new Dictionary<string, DestinationResponse>();
            currentDestinations[clusterIndex].ForEach(destination =>
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
            clusterIndex++;
        });


        return response;
    }

    private async Task<CurrentConfigBaseData?> GetCurrentConfigBaseData(CancellationToken cancellationToken)
    {
        CurrentConfigBaseData? currentConfigBaseData = await context
            .Database
            .SqlQuery<CurrentConfigBaseData>($"""
                                      SELECT 
                                          gc.id AS id,
                                          gc.name_value AS gateway_config_name,
                                          gc.is_current_config AS is_current_config
                                      FROM gateway_configs gc
                                      WHERE gc.is_current_config = true
                                      """)
            .SingleOrDefaultAsync(cancellationToken);
        return currentConfigBaseData;
    }

    private sealed record CurrentConfigBaseData(
        Guid Id,
        string GatewayConfigName,
        bool IsCurrentConfig);

    private sealed record CurrentRoute(
        Guid Id,
        string RouteName,
        string ClusterName,
        string MatchPath,
        Transforms Transforms);
    
    private sealed record Transforms(
        Guid Id,
        List<Dictionary<string, string>> TransformItems);
    
    private sealed record TransformItem(string Key, string Value);

    private sealed record CurrentClusters(
        Guid Id,
        string ClusterName);

    private sealed record Destination(
        Guid Id,
        string DestinationName,
        string Address);
}
