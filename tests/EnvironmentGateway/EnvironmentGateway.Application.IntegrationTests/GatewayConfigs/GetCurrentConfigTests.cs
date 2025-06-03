using EnvironmentGateway.Application.GatewayConfigs.GetCurrentConfig;
using EnvironmentGateway.Application.IntegrationTests.Infrastructure;
using EnvironmentGateway.Domain.Abstractions;
using EnvironmentGateway.Domain.GatewayConfigs;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace EnvironmentGateway.Application.IntegrationTests.GatewayConfigs;

public class GetCurrentConfigTests(IntegrationTestWebAppFactory factory) 
    : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task GetCurrentConfig_ShouldReturnSuccess_WhenCurrentGatewayConfigExists()
    {
        // Arrange
        var query = new GetCurrentConfigQuery();

        // Act
        var result = await Sender.Send(query);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.IsCurrentConfig.Should().BeTrue();
    }
    
    [Fact]
    public async Task GetCurrentConfig_ShouldReturnFailure_WhenCurrentGatewayConfigNotExists()
    {
        // Arrange 
        var query = new GetCurrentConfigQuery();
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
        var result = await Sender.Send(query);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(Error.NullValue);
    }
}