using EnvironmentGateway.Application.Abstractions.Data;
using EnvironmentGateway.Application.Abstractions.Messaging;
using EnvironmentGateway.Domain.Abstractions;
using EnvironmentGateway.Domain.GatewayConfigs;
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

        CurrentConfigMapper.CurrentConfigBaseData currentConfigBaseData = await GetCurrentConfigBaseData(cancellationToken);
        
        var currentRoutes = await context
            .Routes
            .Where(route => route.GatewayConfigId == currentConfigBaseData.Id)
            .Select(route => new CurrentConfigMapper.CurrentRoute(
                    route.Id,
                    route.RouteName.Value,
                    route.ClusterName.Value,
                    route.Match.Path.Value,
                    new CurrentConfigMapper.Transforms(
                        route.Transforms.Id,
                        route.Transforms.Transforms
                            .Select(item => new Dictionary<string, string>{{item.Key, item.Value}})
                            .ToList()
                        )
            ))
            .ToListAsync(cancellationToken);

        var currentClusters = await context
            .Clusters
            .Where(cluster => cluster.GatewayConfigId == currentConfigBaseData.Id)
            .Select(cluster => new CurrentConfigMapper.CurrentCluster(
                cluster.Id,
                cluster.ClusterName.Value,
                new List<CurrentConfigMapper.Destination>(
                    cluster.Destinations
                        .Select(destination => new CurrentConfigMapper.Destination(
                            destination.Id,
                            destination.DestinationName.Value,
                            destination.Address.Value)
                        )
                        .ToList()
                )
            ))
            .ToListAsync(cancellationToken);
        

        CurrentConfigResponse response = CurrentConfigMapper.MapCurrentConfigResponse(currentConfigBaseData, currentRoutes, currentClusters);


        return response;
    }

    

    private async Task<CurrentConfigMapper.CurrentConfigBaseData?> GetCurrentConfigBaseData(CancellationToken cancellationToken)
    {
        CurrentConfigMapper.CurrentConfigBaseData? currentConfigBaseData = await context
            .Database
            .SqlQuery<CurrentConfigMapper.CurrentConfigBaseData>($"""
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

    
}
