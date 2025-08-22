using System.Net;
using System.Net.Http.Json;
using EnvironmentGateway.Api.FunctionalTests.Infrastructure;
using EnvironmentGateway.Domain.GatewayConfigs;
using FluentAssertions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace EnvironmentGateway.Api.FunctionalTests.CurrentConfig;

public class GetTests(FunctionalTestWebAppFactory factory) : BaseFunctionalTest(factory)
{
    [Fact]
    public async Task GetCurrentConfig_ShouldReturnCurrentConfig_WhenRequestIsValid()
    {
        // Arrange
        var accessToken = await GetAccessTokenAsync();
        HttpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue(
                JwtBearerDefaults.AuthenticationScheme, accessToken);
        await CreateTestConfigAsync();
        // Act
        var httpResponse = await HttpClient.GetAsync("current-config");

        // Assert
        httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetCurrentConfig_ShouldReturnNotFound_WhenCurrentConfigNotExists()
    {
        // Arrange
        var accessToken = await GetAccessTokenAsync();
        HttpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue(
                JwtBearerDefaults.AuthenticationScheme, accessToken);
        await DeleteCurrentConfig();
        // Act
        var httpResponse = await HttpClient.GetAsync("current-Config");

        // Assert
        httpResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
