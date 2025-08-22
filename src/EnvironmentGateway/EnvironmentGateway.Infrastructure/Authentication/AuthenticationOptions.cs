namespace EnvironmentGateway.Infrastructure.Authentication;

public class AuthenticationOptions
{
    public string AuthorizationUrl { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string MetadataAddress { get; set; }
}
