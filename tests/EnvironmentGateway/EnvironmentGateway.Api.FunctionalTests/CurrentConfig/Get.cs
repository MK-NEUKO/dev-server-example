using System.Net;
using System.Net.Http.Json;
using EnvironmentGateway.Api.FunctionalTests.Infrastructure;
using EnvironmentGateway.Application.GatewayConfigs.GetStartConfig;
using FluentAssertions;
using Microsoft.AspNetCore.Http;

namespace EnvironmentGateway.Api.FunctionalTests.CurrentConfig;

public class Get : BaseFunctionalTest
{
    public Get(FunctionalTestWebAppFactory factory) 
        : base(factory)
    {
    }

    [Fact]
    public async Task GetCurrentConfig_ShouldReturnCurrentConfig_WhenRequestIsValid()
    {
        // Arrange

        // Act
        var httpResponse = await HttpClient.GetAsync("current-config");

        // Assert
        httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var response = await httpResponse.Content.ReadFromJsonAsync<StartConfigResponse>();
        response.IsCurrentConfig.Should().BeTrue();
    }
}