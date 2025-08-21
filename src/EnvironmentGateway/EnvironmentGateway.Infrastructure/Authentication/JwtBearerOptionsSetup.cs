using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace EnvironmentGateway.Infrastructure.Authentication;

internal sealed class JwtBearerOptionsSetup 
    : IConfigureNamedOptions<JwtBearerOptions>
{
    private readonly AuthenticationOptions _authenticationOptions;
    private readonly IHostEnvironment _environment;

    public JwtBearerOptionsSetup(
        IOptions<AuthenticationOptions> authenticationOptions,
        IHostEnvironment environment)
    {
        _authenticationOptions = authenticationOptions.Value;
        _environment = environment;
    }
    
    public void Configure(JwtBearerOptions options)
    {
        options.Audience = _authenticationOptions.Audience;
        options.Authority = _authenticationOptions.AuthorizationUrl;
        options.MetadataAddress = _authenticationOptions.MetadataAddress;
        options.TokenValidationParameters.ValidIssuer = _authenticationOptions.Issuer;

        if (_environment.IsDevelopment())
        {
            options.RequireHttpsMetadata = false;
        }
    }

    public void Configure(string? name, JwtBearerOptions options)
    {
        Configure(options);
    }
}
