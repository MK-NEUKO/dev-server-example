using System.Net;
using System.Net.Http.Json;
using EnvironmentGateway.Api.Endpoints.Destinations.ChangeDestinationName;
using EnvironmentGateway.Api.FunctionalTests.Infrastructure;
using EnvironmentGateway.Domain.Clusters.Destinations;
using FluentAssertions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

namespace EnvironmentGateway.Api.FunctionalTests.Destinations;

public class ChangeDestinationNameTests(FunctionalTestWebAppFactory factory) : BaseFunctionalTest(factory)
{
    [Fact]
    public async Task UpdateDestinationName_ShouldReturnOk_WhenRequestIsValid()
    {
        // Arrange
        var accessToken = await GetAccessTokenAsync();
        HttpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue(
                JwtBearerDefaults.AuthenticationScheme, accessToken);
        const string testName = "NewDestinationName";
        await CreateTestConfigAsync();
        Guid clusterId = await DbContext.Clusters
            .Select(c => c.Id)
            .FirstOrDefaultAsync();
        Guid destinationId = await DbContext.Clusters
            .SelectMany(c => c.Destinations)
            .Select(d => d.Id)
            .FirstOrDefaultAsync();
        var request = new ChangeDestinationNameRequest(clusterId, destinationId, testName);

        // Act
        HttpResponseMessage response = await HttpClient.PutAsJsonAsync("change-destination-name", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        RenewDbContext();
        var cluster = await DbContext.Clusters
            .Include(c => c.Destinations)
            .FirstOrDefaultAsync(c => c.Id == clusterId);
        cluster.Should().NotBeNull();
        Destination? updatedDestination = cluster.Destinations.FirstOrDefault(d => d.Id == destinationId);
        updatedDestination.Should().NotBeNull();
        updatedDestination.DestinationName.Value.Should().Be(testName);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("!")]
    [InlineData("@invalid")]
    [InlineData("NameWith#Hash")]
    [InlineData("NameWith$Dollar")]
    [InlineData("NameWith%Percent")]
    [InlineData("NameWith^Caret")]
    [InlineData("NameWith&And")]
    [InlineData("NameWith*Star")]
    public async Task UpdateDestinationName_ShouldReturnBadRequest_WhenDestinationNameIsInvalid(string testName)
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
        var request = new ChangeDestinationNameRequest(clusterId, destinationId, testName);

        // Act
        HttpResponseMessage response = await HttpClient.PutAsJsonAsync("change-destination-name", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
