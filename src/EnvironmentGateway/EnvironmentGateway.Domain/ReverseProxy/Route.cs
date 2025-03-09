namespace EnvironmentGatewayDomain.NewFolder;

public sealed class Route 
{
    public string RouteId { get; init; }
    public RouteMatch Match { get; set; }
    public string Order { get; set; }
    public string ClusterId { get; set; }
    public string AuthorizationPolicy { get; set; }
    public string RateLimiterPolicy { get; set; }
    public string OuteputCachePolicy { get; set; }
    public string TimeoutPolicy { get; set; }
    public string Timeout { get; set; }
    public string CorsPolicy { get; set; }
    public string MaxRequestBodySize { get; set; }
    public string Metadata { get; set; }
    public string Transforms { get; set; }
}