using EnvironmentGateway.Application.Abstractions.Messaging;
using EnvironmentGateway.Application.Destinations.UpdateDestination;
using EnvironmentGateway.Application.IntegrationTests.Infrastructure;
using EnvironmentGateway.Domain.Destinations;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EnvironmentGateway.Application.IntegrationTests.UpdateConfig.UpdateDestination;

public class UpdateDestinationTests(
    IntegrationTestWebAppFactory factory) 
    : BaseIntegrationTest(factory)
{
    private readonly ICommandHandler<UpdateDestinationCommand> _handler 
        = factory.Services.GetRequiredService<ICommandHandler<UpdateDestinationCommand>>();

    [Fact]
    public async Task UpdateDestination_ShouldReturnSuccess_WhenDestinationAddressUpdated()
    {
        // Arrange
        const string testAddress = "https://example.com";
        var destinationId = await DbContext.Destinations
            .Select(d => d.Id)
            .FirstOrDefaultAsync();
        var request = new UpdateDestinationCommand(destinationId, testAddress);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        var updatedDestination = await DbContext.Destinations.FirstOrDefaultAsync(d => d.Id == destinationId);
        updatedDestination.Should().NotBeNull();
        updatedDestination.Address.Value.Should().Be(testAddress);
    }

    [Fact]
    public async Task UpdateDestination_ShouldReturnFailure_WhenDestinationIdNotExits()
    {
        // Arrange
        const string testAddress = "https://example.com";
        var request = new UpdateDestinationCommand(Guid.NewGuid(), testAddress);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        var testDestination = await DbContext.Destinations.FirstOrDefaultAsync();
        testDestination.Should().NotBeNull();
        testDestination.Address.Value.Should().NotBe(testAddress);
    }
}
