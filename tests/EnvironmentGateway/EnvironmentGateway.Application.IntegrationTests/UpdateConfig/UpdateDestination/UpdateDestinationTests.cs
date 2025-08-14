using EnvironmentGateway.Application.Abstractions.Messaging;
using EnvironmentGateway.Application.Destinations.UpdateDestination;
using EnvironmentGateway.Application.IntegrationTests.Infrastructure;
using EnvironmentGateway.Domain.Abstractions;
using EnvironmentGateway.Domain.Clusters.Destinations;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EnvironmentGateway.Application.IntegrationTests.UpdateConfig.UpdateDestination;

public class UpdateDestinationTests : BaseIntegrationTest
{
    private readonly ICommandHandler<UpdateDestinationCommand> _handler;
    
    public UpdateDestinationTests(IntegrationTestWebAppFactory factory) 
        : base(factory)
    {
        _handler = Scope.ServiceProvider.GetRequiredService<ICommandHandler<UpdateDestinationCommand>>();
    }
    
    
    [Fact]
    public async Task UpdateDestination_ShouldReturnSuccess_WhenDestinationAddressUpdated()
    {
        // Arrange
        const string testAddress = "https://example.com";
        Guid destinationId = await DbContext.Clusters
            .SelectMany(c => c.Destinations)
            .Select(d => d.Id)
            .FirstOrDefaultAsync();
        Guid clusterId = await DbContext.Clusters
            .Select(c => c.Id)
            .FirstOrDefaultAsync();
        var request = new UpdateDestinationCommand(destinationId, clusterId, testAddress);

        // Act
        Result result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        Destination? updatedDestination = await DbContext.Clusters
            .SelectMany(c => c.Destinations)
            .FirstOrDefaultAsync(d => d.Id == destinationId);
        updatedDestination.Should().NotBeNull();
        updatedDestination.Address.Value.Should().Be(testAddress);
    }

    [Fact]
    public async Task UpdateDestination_ShouldReturnFailure_WhenDestinationIdNotExits()
    {
        // Arrange
        const string testAddress = "https://example.com";
        var request = new UpdateDestinationCommand(Guid.NewGuid(), Guid.NewGuid(), testAddress);

        // Act
        Result result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        Destination? testDestination = await DbContext.Clusters
            .SelectMany(c => c.Destinations)
            .FirstOrDefaultAsync();
        testDestination.Should().NotBeNull();
        testDestination.Address.Value.Should().NotBe(testAddress);
    }
}
