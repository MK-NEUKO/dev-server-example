using System.Net;
using System.Net.Http.Json;
using EnvironmentGateway.Api.Endpoints.UpdateConfig.UpdateDestination;
using EnvironmentGateway.Api.FunctionalTests.Infrastructure;
using EnvironmentGateway.Domain.GatewayConfigs;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace EnvironmentGateway.Api.FunctionalTests.UpdateConfig.UpdateDestination;

public class UpdateDestinationTests(FunctionalTestWebAppFactory factory) : BaseFunctionalTest(factory)
{
    [Fact]
    public async Task UpdateDestination_ShouldReturnOk_WhenRequestIsValid()
    {
        // Arrange
        const string testAddress = "https://example.com";
        var destinationId = await DbContext.Destinations
            .Select(d => d.Id)
            .FirstOrDefaultAsync();
        var request = new UpdateDestinationRequest(destinationId, testAddress);

        // Act
        var response = await HttpClient.PutAsJsonAsync("update-destination", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var updatedDestination = await DbContext.Destinations.FirstOrDefaultAsync(d => d.Id == destinationId);
        updatedDestination.Should().NotBeNull();
        updatedDestination!.Address.Value.Should().Be(testAddress);
    }
}