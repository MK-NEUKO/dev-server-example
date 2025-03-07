namespace EnvironmentGatewayDomain.NewFolder;

public sealed class SessionAffinity
{
    public bool Enabled { get; set; }
    public string Policy { get; set; }
    public string FailurePolicy { get; set; }
    public string AffinityKeyName { get; set; }
    public string Cookie { get; set; }
}