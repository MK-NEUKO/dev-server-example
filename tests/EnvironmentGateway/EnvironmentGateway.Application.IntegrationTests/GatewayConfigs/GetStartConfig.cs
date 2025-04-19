using EnvironmentGateway.Application.GatewayConfigs.GetStartConfig;
using EnvironmentGateway.Application.IntegrationTests.Infrastructure;
using EnvironmentGateway.Domain.Abstractions;
using EnvironmentGateway.Domain.GatewayConfigs;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace EnvironmentGateway.Application.IntegrationTests.GatewayConfigs;

public class GetStartConfig : BaseIntegrationTest
{
    public GetStartConfig(IntegrationTestWebAppFactory factory) 
        : base(factory)
    {
    }
    
    [Fact]
    public async Task GetStartConfig_ShouldReturnFailure_WhenCurrentGatewayConfigNotExists()
    {
        // Arrange 
        var isCurrentConfig = true; 
        var query = new GetStartConfigQuery(isCurrentConfig);
        var existingConfig = await DbContext.GatewayConfigs.FirstOrDefaultAsync();
        if (existingConfig != null)
        {
            DbContext.GatewayConfigs.Remove(existingConfig);
            await DbContext.SaveChangesAsync();
        }

        // Act 
        var result = await Sender.Send(query);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(Error.NullValue);
    }

    [Fact]
    public async Task GetStartConfig_ShouldReturnSuccess_WhenCurrentGatewayConfigExists()
    {
        // Arrange
        var isCurrentConfig = true;
        var query = new GetStartConfigQuery(isCurrentConfig);

        // Act
        var result = await Sender.Send(query);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.IsCurrentConfig.Should().BeTrue();
    }
}