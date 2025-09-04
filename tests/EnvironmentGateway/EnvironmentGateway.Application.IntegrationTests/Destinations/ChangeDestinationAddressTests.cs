using EnvironmentGateway.Application.Abstractions.Messaging;
using EnvironmentGateway.Application.Destinations.ChangeDestinationAddress;
using EnvironmentGateway.Application.IntegrationTests.Infrastructure;
using EnvironmentGateway.Domain.Abstractions;
using EnvironmentGateway.Domain.Clusters.Destinations;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EnvironmentGateway.Application.IntegrationTests.Destinations;

public class ChangeDestinationAddressTests : BaseIntegrationTest
{
    private readonly ICommandHandler<ChangeDestinationAddressCommand> _handler;
    private const string TestAddress = "https://test-address.com";

    public ChangeDestinationAddressTests(IntegrationTestWebAppFactory factory) 
        : base(factory)
    {
        _handler = Scope.ServiceProvider.GetRequiredService<ICommandHandler<ChangeDestinationAddressCommand>>();
    }
    
    
    [Fact]
    public async Task ChangeDestinationAddress_ShouldReturnSuccess_WhenDestinationAddressWasChanged()
    {
        // Arrange
        Guid destinationId = await DbContext.Clusters
            .SelectMany(c => c.Destinations)
            .Select(d => d.Id)
            .FirstOrDefaultAsync();
        Guid clusterId = await DbContext.Clusters
            .Select(c => c.Id)
            .FirstOrDefaultAsync();
        var request = new ChangeDestinationAddressCommand(clusterId, destinationId, TestAddress);

        // Act
        Result result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        var cluster = await DbContext.Clusters
            .Include(c => c.Destinations)
            .FirstOrDefaultAsync(c => c.Id == clusterId);
        cluster.Should().NotBeNull();
        Destination? updatedDestination = cluster!.Destinations.FirstOrDefault(d => d.Id == destinationId);
        updatedDestination.Should().NotBeNull();
        updatedDestination.Address.Value.Should().Be(TestAddress);
    }

    [Fact]
    public async Task ChangeDestinationAddress_ShouldReturnFailure_WhenDestinationDoesNotExist()
    {
        // Arrange
        var request = new ChangeDestinationAddressCommand(Guid.NewGuid(), Guid.NewGuid(), TestAddress);

        // Act
        Result result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().NotBeNull();
    }

    [Fact]
    public async Task ChangeDestinationAddress_ShouldReturnFailure_WhenClusterDoesNotExist()
    {
        // Arrange
        Guid destinationId = await DbContext.Clusters
            .SelectMany(c => c.Destinations)
            .Select(d => d.Id)
            .FirstOrDefaultAsync();
        var request = new ChangeDestinationAddressCommand(Guid.NewGuid(), destinationId, TestAddress);

        // Act
        Result result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
    }
}
