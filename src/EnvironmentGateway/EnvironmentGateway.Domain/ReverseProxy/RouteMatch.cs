namespace EnvironmentGatewayDomain.NewFolder;

public sealed class RouteMatch
{
    public string Methods { get; set; }
    public string Host { get; set; }
    public string Path { get; set; }
    public string QueryParameters { get; set; }
    public string Headers { get; set; }
}