using EnvironmentGateway.Application.Abstractions.Messaging;
using EnvironmentGateway.Application.Clusters.ChangeClusterName;
using EnvironmentGateway.Application.IntegrationTests.Infrastructure;
using EnvironmentGateway.Domain.Abstractions;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EnvironmentGateway.Application.IntegrationTests.Clusters;

public class ChangeClusterNameTests : BaseIntegrationTest
{
    private readonly ICommandHandler<ChangeClusterNameCommand> _handler;
    private const string TestName = "TestClusterName";

    public ChangeClusterNameTests(IntegrationTestWebAppFactory factory)
        : base(factory)
    {
        _handler = Scope.ServiceProvider.GetRequiredService<ICommandHandler<ChangeClusterNameCommand>>();
    }

    [Fact]
    public async Task ChangeClusterName_ShouldReturnSuccess_WhenClusterNameWasChanged()
    {
        // Arrange
        Guid clusterId = await DbContext.Clusters
            .Select(c => c.Id)
            .FirstOrDefaultAsync();
        var request = new ChangeClusterNameCommand(clusterId, TestName);

        // Act
        Result result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();

        var cluster = await DbContext.Clusters
            .FirstOrDefaultAsync(c => c.Id == clusterId);
        cluster.Should().NotBeNull();
        cluster!.ClusterName.Value.Should().Be(TestName);
    }

    [Fact]
    public async Task ChangeClusterName_ShouldReturnFailure_WhenClusterDoesNotExist()
    {
        // Arrange
        var request = new ChangeClusterNameCommand(Guid.NewGuid(), TestName);

        // Act
        Result result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().NotBeNull();
    }
}
