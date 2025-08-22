using System.Net;
using System.Net.Http.Json;
using EnvironmentGateway.Api.Endpoints.Destinations.ChangeDestinationAddress;
using EnvironmentGateway.Api.FunctionalTests.Infrastructure;
using EnvironmentGateway.Domain.Clusters.Destinations;
using EnvironmentGateway.Infrastructure;
using FluentAssertions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

namespace EnvironmentGateway.Api.FunctionalTests.Destinations;

public class ChangeDestinationAddressTests(FunctionalTestWebAppFactory factory) : BaseFunctionalTest(factory)
{
    [Fact]
    public async Task UpdateDestination_ShouldReturnOk_WhenRequestIsValid()
    {
        // Arrange
        var accessToken = await GetAccessTokenAsync();
        HttpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue(
                JwtBearerDefaults.AuthenticationScheme, accessToken);
        const string testAddress = "https://example.com";
        await CreateTestConfigAsync();
        Guid clusterId = await DbContext.Clusters
            .Select(c => c.Id)
            .FirstOrDefaultAsync();
        Guid destinationId = await DbContext.Clusters
            .SelectMany(c => c.Destinations)
            .Select(d => d.Id)
            .FirstOrDefaultAsync();
        var request = new ChangeDestinationAddressRequest(clusterId, destinationId, testAddress);

        // Act
        HttpResponseMessage response = await HttpClient.PutAsJsonAsync("update-destination", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        RenewDbContext();
        var cluster = await DbContext.Clusters
            .Include(c => c.Destinations)
            .FirstOrDefaultAsync(c => c.Id == clusterId);
        cluster.Should().NotBeNull();
        Destination? updatedDestination = cluster.Destinations.FirstOrDefault(d => d.Id == destinationId);
        updatedDestination.Should().NotBeNull();
        updatedDestination.Address.Value.Should().Be(testAddress);
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
        var accessToken = await GetAccessTokenAsync();
        HttpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue(
                JwtBearerDefaults.AuthenticationScheme, accessToken);
        await CreateTestConfigAsync();
        Guid destinationId = await DbContext.Clusters
            .SelectMany(c => c.Destinations)
            .Select(d => d.Id)
            .FirstOrDefaultAsync();
        Guid clusterId = await DbContext.Clusters
            .Select(c => c.Id)
            .FirstOrDefaultAsync();
        var request = new ChangeDestinationAddressRequest(clusterId, destinationId, testAddress);

        // Act
        HttpResponseMessage response = await HttpClient.PutAsJsonAsync("update-destination", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
