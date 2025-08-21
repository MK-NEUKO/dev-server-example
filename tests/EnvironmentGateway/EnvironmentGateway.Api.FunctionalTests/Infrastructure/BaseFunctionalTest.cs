using System.Net.Http.Json;
using System.Text.Json;
using EnvironmentGateway.Infrastructure;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.Extensions.DependencyInjection;

namespace EnvironmentGateway.Api.FunctionalTests.Infrastructure;

public abstract class BaseFunctionalTest : IClassFixture<FunctionalTestWebAppFactory>
{
    protected readonly HttpClient HttpClient;
    protected readonly EnvironmentGatewayDbContext DbContext;
    protected readonly string KeycloakBaseUrl;

    protected BaseFunctionalTest(FunctionalTestWebAppFactory factory)
    {
        HttpClient = factory.CreateClient();
        IServiceScope scope = factory.Services.CreateScope();
        DbContext = scope.ServiceProvider.GetRequiredService<EnvironmentGatewayDbContext>();
        KeycloakBaseUrl = factory.KeycloakBaseUrl;
    }
    
    protected async Task<string> GetAccessTokenAsync()
    {
        var keycloakTokenUrl = KeycloakBaseUrl + "realms/dev-server-example/protocol/openid-connect/token";
        var requestBody = new Dictionary<string, string>
        {
            { "client_id", "production-gateway-api" },
            { "username", "mkneuko" },
            { "password", "password123" },
            { "grant_type", "password" }
        };

        using var httpClient = new HttpClient();
        var response = await httpClient.PostAsync(
            keycloakTokenUrl,
            new FormUrlEncodedContent(requestBody)
        );

        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        
        // Nur das access_token extrahieren
        using var doc = JsonDocument.Parse(content);
        return doc.RootElement.GetProperty("access_token").GetString() ?? string.Empty;

    }
}
