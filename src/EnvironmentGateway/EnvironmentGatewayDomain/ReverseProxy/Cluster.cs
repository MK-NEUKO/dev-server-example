namespace EnvironmentGatewayDomain.NewFolder;

public sealed class Cluster
{
    public string ClusterId { get; set; }
    public string LoadeBalancingPolicy { get; set; }
    public SessionAffinity SessionAffinity  { get; set; }
    public string HealthCheck { get; set; }
    public string HttpClient { get; set; }
    public string HttpRequest { get; set; }
    public List<Destination> Destinations { get; set; }
    public string Metadata { get; set; }
}