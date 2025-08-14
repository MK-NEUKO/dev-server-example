using EnvironmentGateway.Application.Destinations.UpdateDestination;
using EnvironmentGateway.Domain.Abstractions;
using EnvironmentGateway.Domain.Clusters;
using EnvironmentGateway.Domain.Clusters.Destinations;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace EnvironmentGateway.Application.UnitTests.Destinations;

public class ChangeDestinationAddressTests
{
    private static readonly Guid ClusterId = Guid.NewGuid();
    private static readonly Guid DestinationId = Guid.NewGuid();
    private const string TestDestinationName = "TestDestination";
    private const string TestClusterName = "TestCluster";
    private const string TestAddress = "Https://test-address.com";

    private static readonly ChangeDestinationAddressCommand AddressCommand = new(
        ClusterId,
        DestinationId,
        TestAddress);

    private readonly ChangeDestinationAddressCommandHandler _handler;
    private readonly IDestinationRepository _destinationRepositoryMock;
    private readonly IUnitOfWork _unitOfWorkMock;

    public ChangeDestinationAddressTests()
    {
        _destinationRepositoryMock = Substitute.For<IDestinationRepository>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();

        _handler = new ChangeDestinationAddressCommandHandler(
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
        var result = await _handler.Handle(AddressCommand, default);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DestinationErrors.NotFound);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenUnitOfWorkThrowsException()
    {
        // Arrange
        _unitOfWorkMock.SaveChangesAsync(
            Arg.Any<CancellationToken>()).Throws(new Exception());
        
        // Act
        var result = await _handler.Handle(AddressCommand, default);

        // Assert
        result.IsFailure.Should().BeTrue();
    }

    
    [Fact]
    public async Task Handle_Should_UpdateDestination_WhenDestinationExists()
    {
        // Arrange
        var cluster = Cluster.Create(TestClusterName, TestDestinationName, TestAddress);
        //var destination = Destination.Create(
        //    DestinationName, TestAddress);
        _destinationRepositoryMock.GetByIdAsync(
            ClusterId, 
            DestinationId, 
            Arg.Any<CancellationToken>()).Returns(cluster.Destinations[0]);

        // Act
        var result = await _handler.Handle(AddressCommand, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        await _destinationRepositoryMock.Received(1).GetByIdAsync(ClusterId, DestinationId, Arg.Any<CancellationToken>());
            await _unitOfWorkMock.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
        cluster.Destinations[0].Address.Value.Should().Be(TestAddress);
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
        var result = await _handler.Handle(AddressCommand, default);

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
        await _handler.Handle(AddressCommand, token);

        // Assert
        await _destinationRepositoryMock.Received(1).GetByIdAsync(ClusterId, DestinationId, token);
        await _unitOfWorkMock.Received(1).SaveChangesAsync(token);
    }
}
