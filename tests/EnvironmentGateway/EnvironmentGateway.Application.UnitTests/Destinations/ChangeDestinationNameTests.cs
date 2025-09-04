using EnvironmentGateway.Application.Destinations.ChangeDestinationName;
using EnvironmentGateway.Domain.Abstractions;
using EnvironmentGateway.Domain.Clusters;
using EnvironmentGateway.Domain.Clusters.Destinations;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace EnvironmentGateway.Application.UnitTests.Destinations;

public class ChangeDestinationNameTests
{
    private static readonly Guid ClusterId = Guid.NewGuid();
    private static readonly Guid DestinationId = Guid.NewGuid();
    private const string TestDestinationName = "TestDestination";
    private const string TestClusterName = "TestCluster";
    private const string TestAddress = "Https://test-address.com";
    private const string NewDestinationName = "NewDestinationName";

    private static readonly ChangeDestinationNameCommand NameCommand = new(
        ClusterId,
        DestinationId,
        NewDestinationName);

    private readonly ChangeDestinationNameCommandHandler _handler;
    private readonly IDestinationRepository _destinationRepositoryMock;
    private readonly IUnitOfWork _unitOfWorkMock;

    public ChangeDestinationNameTests()
    {
        _destinationRepositoryMock = Substitute.For<IDestinationRepository>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();

        _handler = new ChangeDestinationNameCommandHandler(
            _destinationRepositoryMock,
            _unitOfWorkMock);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenDestinationDoesNotExist()
    {
        // Arrange
        _destinationRepositoryMock.GetByIdAsync(
            ClusterId,
            DestinationId,
            Arg.Any<CancellationToken>()).Returns((Destination?)null);

        // Act
        var result = await _handler.Handle(NameCommand, default);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DestinationErrors.NotFound);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenUnitOfWorkThrowsException()
    {
        // Arrange
        var cluster = Cluster.Create(TestClusterName, TestDestinationName, TestAddress);
        _destinationRepositoryMock.GetByIdAsync(
            ClusterId,
            DestinationId,
            Arg.Any<CancellationToken>()).Returns(cluster.Destinations[0]);
        _unitOfWorkMock.SaveChangesAsync(
            Arg.Any<CancellationToken>()).Throws(new Exception());

        // Act & Assert
        await FluentActions.Invoking(() => _handler.Handle(NameCommand, default))
            .Should().ThrowAsync<Exception>();
    }

    [Fact]
    public async Task Handle_Should_UpdateDestinationName_WhenDestinationExists()
    {
        // Arrange
        var cluster = Cluster.Create(TestClusterName, TestDestinationName, TestAddress);
        _destinationRepositoryMock.GetByIdAsync(
            ClusterId,
            DestinationId,
            Arg.Any<CancellationToken>()).Returns(cluster.Destinations[0]);

        // Act
        var result = await _handler.Handle(NameCommand, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        await _destinationRepositoryMock.Received(1).GetByIdAsync(ClusterId, DestinationId, Arg.Any<CancellationToken>());
        await _unitOfWorkMock.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
        cluster.Destinations[0].DestinationName.Value.Should().Be(NewDestinationName);
    }

    [Fact]
    public async Task Handle_Should_NotCallSaveChanges_WhenDestinationDoesNotExist()
    {
        // Arrange
        _destinationRepositoryMock.GetByIdAsync(
            ClusterId,
            DestinationId,
            Arg.Any<CancellationToken>()).Returns((Destination?)null);

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
        _destinationRepositoryMock.GetByIdAsync(
            ClusterId,
            DestinationId,
            Arg.Any<CancellationToken>()).Returns(cluster.Destinations[0]);

        using var cts = new CancellationTokenSource();
        var token = cts.Token;

        // Act
        await _handler.Handle(NameCommand, token);

        // Assert
        await _destinationRepositoryMock.Received(1).GetByIdAsync(ClusterId, DestinationId, token);
        await _unitOfWorkMock.Received(1).SaveChangesAsync(token);
    }
}
