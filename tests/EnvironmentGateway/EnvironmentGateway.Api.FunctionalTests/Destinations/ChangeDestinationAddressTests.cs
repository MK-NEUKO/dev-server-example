using System.Net;
using System.Net.Http.Json;
using EnvironmentGateway.Api.Endpoints.Destinations.ChangeDestinationAddress;
using EnvironmentGateway.Api.FunctionalTests.Infrastructure;
using EnvironmentGateway.Domain.Clusters.Destinations;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace EnvironmentGateway.Api.FunctionalTests.Destinations;

public class ChangeDestinationAddressTests(FunctionalTestWebAppFactory factory) : BaseFunctionalTest(factory)
{
    [Fact]
    public async Task UpdateDestination_ShouldReturnOk_WhenRequestIsValid()
    {
        // Arrange
        const string testAddress = "https://example.com";
        Guid clusterId = await DbContext.Clusters
            .Select(c => c.Id)
            .FirstOrDefaultAsync();
        Guid destinationId = await DbContext.Clusters
            .SelectMany(c => c.Destinations)
            .Select(d => d.Id)
            .FirstOrDefaultAsync();
        var request = new ChangeDestinationAddressRequest(destinationId, clusterId, testAddress);

        // Act
        HttpResponseMessage response = await HttpClient.PutAsJsonAsync("update-destination", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        Destination? updatedDestination = await DbContext.Clusters
            .SelectMany(c => c.Destinations)
            .FirstOrDefaultAsync(d => d.Id == destinationId);
        updatedDestination.Should().NotBeNull();
        updatedDestination!.Address.Value.Should().Be(testAddress);
    }
    
    [Theory]
    [InlineData("htps://example.com")]
    [InlineData("example.com")]
    [InlineData("//example.com")]
    [InlineData("https:/example.com")]
    [InlineData("https//example.com")]
    [InlineData("https://example")]
    [InlineData("https://example..com")]
    [InlineData("https://.com")]
    [InlineData("https://exam ple.com")]
    [InlineData("https://example!.com")]
    public async Task UpdateDestination_ShouldReturnBadRequest_WhenDestinationAddressIsInvalid(string testAddress)
    {
        // Arrange
        Guid destinationId = await DbContext.Clusters
            .SelectMany(c => c.Destinations)
            .Select(d => d.Id)
            .FirstOrDefaultAsync();
        Guid clusterId = await DbContext.Clusters
            .Select(c => c.Id)
            .FirstOrDefaultAsync();
        var request = new ChangeDestinationAddressRequest(destinationId, clusterId, testAddress);

        // Act
        HttpResponseMessage response = await HttpClient.PutAsJsonAsync("update-destination", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
