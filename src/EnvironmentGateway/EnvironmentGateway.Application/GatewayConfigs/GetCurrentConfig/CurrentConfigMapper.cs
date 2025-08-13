namespace EnvironmentGateway.Application.GatewayConfigs.GetCurrentConfig;

internal sealed class CurrentConfigMapper
{
    internal static CurrentConfigResponse Map(
        CurrentConfigBaseData? currentConfigBaseData,
        List<CurrentRoute> currentRoutes, 
        List<CurrentCluster> currentClusters)
    {
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
        
        currentClusters.ForEach(cluster =>
        {
            var destinations = new Dictionary<string, DestinationResponse>();
            cluster.Destinations.ForEach(destination =>
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
    
    internal sealed record CurrentConfigBaseData(
        Guid Id,
        string GatewayConfigName,
        bool IsCurrentConfig);

    internal sealed record CurrentRoute(
        Guid Id,
        string RouteName,
        string ClusterName,
        string MatchPath,
        Transforms Transforms);
    
    internal sealed record Transforms(
        Guid Id,
        List<Dictionary<string, string>> TransformItems);
    
    internal sealed record TransformItem(string Key, string Value);

    internal sealed record CurrentCluster(
        Guid Id,
        string ClusterName,
        List<Destination> Destinations);

    internal sealed record Destination(
        Guid Id,
        string DestinationName,
        string Address);
}
