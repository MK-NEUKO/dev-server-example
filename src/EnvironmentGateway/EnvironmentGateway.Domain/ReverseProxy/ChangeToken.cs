namespace EnvironmentGatewayDomain.NewFolder;

public sealed class ChangeToken
{
    public bool HasChanged { get; set; }
    public bool ActiveChangedCallbacks { get; set; }
}