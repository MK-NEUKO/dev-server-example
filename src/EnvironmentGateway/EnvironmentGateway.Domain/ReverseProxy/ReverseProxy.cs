using System.Security.Cryptography;
using SharedKernel;

namespace EnvironmentGatewayDomain.NewFolder;

public sealed class ReverseProxy : Entity
{
    public string RevisionId { get; set; }
    public List<Route> Routes { get; set; }
    public List<Cluster> Clusters { get; set; }
    public ChangeToken ChangeToken { get; set; }
}