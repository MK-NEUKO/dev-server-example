using EnvironmentGateway.Application.Destinations.UpdateDestination;
using EnvironmentGateway.Domain.Abstractions;
using EnvironmentGateway.Domain.Destinations;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace EnvironmentGateway.Application.UnitTests.Destinations;

public class UpdateDestinationTests
{
    private static readonly Guid Id = Guid.NewGuid();
    private const string TestAddress = "Https://example.com";

    private static readonly UpdateDestinationCommand Command = new(
        Id,
        TestAddress);

    private readonly UpdateDestinationCommandHandler _handler;

    private readonly IDestinationRepository _destinationRepositoryMock;
    private readonly IUnitOfWork _unitOfWorkMock;

    public UpdateDestinationTests()
    {
        _destinationRepositoryMock = Substitute.For<IDestinationRepository>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();

        _handler = new UpdateDestinationCommandHandler(
            _destinationRepositoryMock,
            _unitOfWorkMock);
    }
    
    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenDestinationDoesNotExist()
    {
        // Arrange
        _destinationRepositoryMock.GetByIdAsync(Id, Arg.Any<CancellationToken>()).Returns((Destination?)null);

        // Act
        var result = await _handler.Handle(Command, default);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DestinationErrors.NotFound);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenUnitOfWorkThrowsException()
    {
        // Arrange
        _unitOfWorkMock.SaveChangesAsync(Arg.Any<CancellationToken>()).Throws(new Exception());
        
        // Act
        var result = await _handler.Handle(Command, default);

        // Assert
        result.IsFailure.Should().BeTrue();
    }

    
    [Fact]
    public async Task Handle_Should_UpdateDestination_WhenDestinationExists()
    {
        // Arrange
        var destination = Destination.Create("test", "http://test.com");
        _destinationRepositoryMock.GetByIdAsync(Id, Arg.Any<CancellationToken>()).Returns(destination);

        // Act
        var result = await _handler.Handle(Command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        await _destinationRepositoryMock.Received(1).GetByIdAsync(Id, Arg.Any<CancellationToken>());
        await _unitOfWorkMock.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
        destination.Address.Value.Should().Be(TestAddress);
    }
}