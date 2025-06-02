using EnvironmentGateway.Application.Destinations.UpdateDestination;
using EnvironmentGateway.Application.Exceptions;
using EnvironmentGateway.Domain.Abstractions;
using EnvironmentGateway.Domain.Destinations;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace EnvironmentGateway.Application.UnitTests.Destinations;

public class UpdateDestinationTests
{
    private static readonly Guid Id = Guid.NewGuid();
    private static string _testAddress = "Https://example.com";

    private static UpdateDestinationCommand _command = new(
        Id,
        _testAddress);

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
        var result = await _handler.Handle(_command, default);

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
        var result = await _handler.Handle(_command, default);

        // Assert
        result.IsFailure.Should().BeTrue();
    }

}