using EnvironmentGateway.Application.Clusters.ChangeClusterName;
using EnvironmentGateway.Domain.Abstractions;
using EnvironmentGateway.Domain.Clusters;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace EnvironmentGateway.Application.UnitTests.Clusters;

public class ChangeClusterNameTests
{
    private static readonly Guid ClusterId = Guid.NewGuid();
    private const string TestClusterName = "TestCluster";
    private const string TestDestinationName = "TestDestination";
    private const string TestAddress = "https://test-address.com";
    private const string NewClusterName = "NewClusterName";

    private static readonly ChangeClusterNameCommand NameCommand = new(
        ClusterId,
        NewClusterName);

    private readonly ChangeClusterNameCommandHandler _handler;
    private readonly IClusterRepository _clusterRepositoryMock;
    private readonly IUnitOfWork _unitOfWorkMock;

    public ChangeClusterNameTests()
    {
        _clusterRepositoryMock = Substitute.For<IClusterRepository>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();

        _handler = new ChangeClusterNameCommandHandler(
            _clusterRepositoryMock,
            _unitOfWorkMock);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenClusterDoesNotExist()
    {
        // Arrange
        _clusterRepositoryMock.GetByIdAsync(
            ClusterId,
            Arg.Any<CancellationToken>()).Returns((Cluster?)null);

        // Act
        var result = await _handler.Handle(NameCommand, default);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(ClusterErrors.NotFound);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenUnitOfWorkThrowsException()
    {
        // Arrange
        var cluster = Cluster.Create(TestClusterName, TestDestinationName, TestAddress);
        _clusterRepositoryMock.GetByIdAsync(
            ClusterId,
            Arg.Any<CancellationToken>()).Returns(cluster);
        _unitOfWorkMock.SaveChangesAsync(
            Arg.Any<CancellationToken>()).Throws(new Exception());

        // Act & Assert
        await FluentActions.Invoking(() => _handler.Handle(NameCommand, default))
            .Should().ThrowAsync<Exception>();
    }

    [Fact]
    public async Task Handle_Should_UpdateClusterName_WhenClusterExists()
    {
        // Arrange
        var cluster = Cluster.Create(TestClusterName, TestDestinationName, TestAddress);
        _clusterRepositoryMock.GetByIdAsync(
            ClusterId,
            Arg.Any<CancellationToken>()).Returns(cluster);

        // Act
        var result = await _handler.Handle(NameCommand, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        await _clusterRepositoryMock.Received(1).GetByIdAsync(ClusterId, Arg.Any<CancellationToken>());
        await _unitOfWorkMock.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
        cluster.ClusterName.Value.Should().Be(NewClusterName);
    }

    [Fact]
    public async Task Handle_Should_NotCallSaveChanges_WhenClusterDoesNotExist()
    {
        // Arrange
        _clusterRepositoryMock.GetByIdAsync(
            ClusterId,
            Arg.Any<CancellationToken>()).Returns((Cluster?)null);

        // Act
        var result = await _handler.Handle(NameCommand, default);

        // Assert
        await _unitOfWorkMock.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
        result.IsFailure.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Should_PropagateCancellationToken()
    {
        // Arrange
        var cluster = Cluster.Create(TestClusterName, TestDestinationName, TestAddress);
        _clusterRepositoryMock.GetByIdAsync(
            ClusterId,
            Arg.Any<CancellationToken>()).Returns(cluster);

        using var cts = new CancellationTokenSource();
        var token = cts.Token;

        // Act
        await _handler.Handle(NameCommand, token);

        // Assert
        await _clusterRepositoryMock.Received(1).GetByIdAsync(ClusterId, token);
        await _unitOfWorkMock.Received(1).SaveChangesAsync(token);
    }
}
