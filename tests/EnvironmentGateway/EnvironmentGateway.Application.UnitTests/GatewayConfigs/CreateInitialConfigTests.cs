using EnvironmentGateway.Application.Exceptions;
using EnvironmentGateway.Application.GatewayConfigs.CreateInitialConfig;
using EnvironmentGateway.Domain.Abstractions;
using EnvironmentGateway.Domain.GatewayConfigs;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace EnvironmentGateway.Application.UnitTests.GatewayConfigs;

public class CreateInitialConfigTests
{
    private static readonly CreateInitialConfigCommand Command = new();
    private readonly CreateInitialConfigCommandHandler _handler;

    private readonly IGatewayConfigRepository _gatewayConfigRepositoryMock;
    private readonly IUnitOfWork _unitOfWorkMock;

    public CreateInitialConfigTests()
    {
        _gatewayConfigRepositoryMock = Substitute.For<IGatewayConfigRepository>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();

        _handler = new CreateInitialConfigCommandHandler(
            _gatewayConfigRepositoryMock,
            _unitOfWorkMock);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenUnitOfWorkThrows()
    {
        // Arrange
        _unitOfWorkMock
            .SaveChangesAsync(Arg.Any<CancellationToken>())
            .ThrowsAsync(new ConcurrencyException("Concurrency", new Exception()));

        // Act
        var result = await _handler.Handle(Command, default);

        // Assert
        result.Error.Should().Be(GatewayConfigErrors.CreateInitialConfigFailed);
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenCurrentConfigExists()
    {
        // Arrange
        var testId = Guid.NewGuid();
        _gatewayConfigRepositoryMock
            .GetCurrentConfigId(Arg.Any<CancellationToken>())
            .Returns(testId);

        // Act
        var result = await _handler.Handle(Command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(testId);
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenCreateInitialConfiguration()
    {
        // Arrange
        await _unitOfWorkMock
            .SaveChangesAsync(Arg.Any<CancellationToken>());

        // Act
        var result = await _handler.Handle(Command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }
}