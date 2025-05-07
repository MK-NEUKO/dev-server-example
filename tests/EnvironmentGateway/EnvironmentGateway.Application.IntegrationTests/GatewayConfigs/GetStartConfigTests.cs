using EnvironmentGateway.Application.GatewayConfigs.GetCurrentConfig;
using EnvironmentGateway.Application.IntegrationTests.Infrastructure;
using EnvironmentGateway.Domain.Abstractions;
using EnvironmentGateway.Domain.GatewayConfigs;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace EnvironmentGateway.Application.IntegrationTests.GatewayConfigs;

public class GetStartConfigTests(IntegrationTestWebAppFactory factory) 
    : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task GetStartConfig_ShouldReturnSuccess_WhenCurrentGatewayConfigExists()
    {
        // Arrange
        var isCurrentConfig = true;
        var query = new GetCurrentConfigQuery();
        
        DbContext.GatewayConfigs.Add(GatewayConfig.CreateNewConfig());
        await DbContext.SaveChangesAsync();

        // Act
        var result = await Sender.Send(query);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.IsCurrentConfig.Should().BeTrue();
    }
    
    [Fact]
    public async Task GetStartConfig_ShouldReturnFailure_WhenCurrentGatewayConfigNotExists()
    {
        // Arrange 
        var isCurrentConfig = true; 
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