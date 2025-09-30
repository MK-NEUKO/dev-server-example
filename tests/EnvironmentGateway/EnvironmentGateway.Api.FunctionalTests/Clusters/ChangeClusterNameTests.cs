using System.Net;
using System.Net.Http.Json;
using EnvironmentGateway.Api.Endpoints.Clusters.ChangeClusterName;
using EnvironmentGateway.Api.FunctionalTests.Infrastructure;
using FluentAssertions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

namespace EnvironmentGateway.Api.FunctionalTests.Clusters;

public class ChangeClusterNameTests(FunctionalTestWebAppFactory factory) : BaseFunctionalTest(factory)
{
    [Fact]
    public async Task ChangeClusterName_ShouldReturnOk_WhenRequestIsValid()
    {
        // Arrange
        var accessToken = await GetAccessTokenAsync();
        HttpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue(
                JwtBearerDefaults.AuthenticationScheme, accessToken);
        const string testName = "NewClusterName";
        await CreateTestConfigAsync();
        Guid clusterId = await DbContext.Clusters
            .Select(c => c.Id)
            .FirstOrDefaultAsync();
        var request = new ChangeClusterNameRequest(clusterId, testName);

        // Act
        HttpResponseMessage response = await HttpClient.PutAsJsonAsync("change-cluster-name", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        RenewDbContext();
        var cluster = await DbContext.Clusters
            .FirstOrDefaultAsync(c => c.Id == clusterId);
        cluster.Should().NotBeNull();
        cluster!.ClusterName.Value.Should().Be(testName);
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
    public async Task ChangeClusterName_ShouldReturnBadRequest_WhenClusterNameIsInvalid(string testName)
    {
        // Arrange
        var accessToken = await GetAccessTokenAsync();
        HttpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue(
                JwtBearerDefaults.AuthenticationScheme, accessToken);
        await CreateTestConfigAsync();
        Guid clusterId = await DbContext.Clusters
            .Select(c => c.Id)
            .FirstOrDefaultAsync();
        var request = new ChangeClusterNameRequest(clusterId, testName);

        // Act
        HttpResponseMessage response = await HttpClient.PutAsJsonAsync("change-cluster-name", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
