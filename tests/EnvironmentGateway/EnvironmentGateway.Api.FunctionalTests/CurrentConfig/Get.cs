using System.Net;
using System.Net.Http.Json;
using EnvironmentGateway.Api.FunctionalTests.Infrastructure;
using EnvironmentGateway.Domain.GatewayConfigs;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

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
        DbContext.GatewayConfigs.Add(GatewayConfig.CreateNewConfig());
        await DbContext.SaveChangesAsync();
        // Act
        var httpResponse = await HttpClient.GetAsync("current-config");

        // Assert
        httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetCurrentConfig_ShouldReturnNotFound_WhenCurrentConfigNotExists()
    {
        // Arrange
        var existingConfigs = await DbContext.GatewayConfigs
            .Where(gc => gc.IsCurrentConfig)
            .ToListAsync();
        foreach (var existingConfig in existingConfigs)
        {
            if (!existingConfig.IsCurrentConfig) continue;
            DbContext.GatewayConfigs.Remove(existingConfig);
            await DbContext.SaveChangesAsync();
        }

        // Act
        var httpResponse = await HttpClient.GetAsync("current-Config");

        // Assert
        httpResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}