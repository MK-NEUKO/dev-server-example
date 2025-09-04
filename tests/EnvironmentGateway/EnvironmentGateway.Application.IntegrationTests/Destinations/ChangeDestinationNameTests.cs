using EnvironmentGateway.Application.Abstractions.Messaging;
using EnvironmentGateway.Application.Destinations.ChangeDestinationName;
using EnvironmentGateway.Application.IntegrationTests.Infrastructure;
using EnvironmentGateway.Domain.Abstractions;
using EnvironmentGateway.Domain.Clusters.Destinations;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EnvironmentGateway.Application.IntegrationTests.Destinations;

public class ChangeDestinationNameTests : BaseIntegrationTest
{
    private readonly ICommandHandler<ChangeDestinationNameCommand> _handler;
    private const string TestName = "TestDestinationName";

    public ChangeDestinationNameTests(IntegrationTestWebAppFactory factory)
        : base(factory)
    {
        _handler = Scope.ServiceProvider.GetRequiredService<ICommandHandler<ChangeDestinationNameCommand>>();
    }

    [Fact]
    public async Task ChangeDestinationName_ShouldReturnSuccess_WhenDestinationNameWasChanged()
    {
        // Arrange
        Guid destinationId = await DbContext.Clusters
            .SelectMany(c => c.Destinations)
            .Select(d => d.Id)
            .FirstOrDefaultAsync();
        Guid clusterId = await DbContext.Clusters
            .Select(c => c.Id)
            .FirstOrDefaultAsync();
        var request = new ChangeDestinationNameCommand(clusterId, destinationId, TestName);

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
        updatedDestination.DestinationName.Value.Should().Be(TestName);
    }

    [Fact]
    public async Task ChangeDestinationName_ShouldReturnFailure_WhenDestinationDoesNotExist()
    {
        // Arrange
        var request = new ChangeDestinationNameCommand(Guid.NewGuid(), Guid.NewGuid(), TestName);

        // Act
        Result result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().NotBeNull();
    }

    [Fact]
    public async Task ChangeDestinationName_ShouldReturnFailure_WhenClusterDoesNotExist()
    {
        // Arrange
        Guid destinationId = await DbContext.Clusters
            .SelectMany(c => c.Destinations)
            .Select(d => d.Id)
            .FirstOrDefaultAsync();
        var request = new ChangeDestinationNameCommand(Guid.NewGuid(), destinationId, TestName);

        // Act
        Result result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
    }
}
