﻿namespace Weather.Tests;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TestAuthentication;
using Xunit;

[Collection(ApiCollectionFixture.Id)]
public class WeatherForecastTests(TestAuthenticationWebApplicationFactory<Program> factory)
{
    private readonly TestAuthenticationWebApplicationFactory<Program> _factory = factory;

    [Fact]
    public async Task Authorized_user_with_access_as_user_scope_can_get_the_forecast()
    {
        // Arrange
        var client = _factory
            .WithTestSchemeAuth([
                   new("scp", "access_as_user")
            ])
            .CreateClient();

        // Act
        var response = await client.GetAsync("WeatherForecast", TestContext.Current.CancellationToken);

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
        var weather = await response.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);
        Assert.Contains("temperatureF", weather);
    }

    [Fact]
    public async Task Authorized_user_without_access_as_user_scope_cannot_get_the_forecast()
    {
        // Arrange
        var client = _factory
            .WithTestSchemeAuth([
                   new("scp", "wrong")
            ])
            .CreateClient();

        // Act
        var response = await client.GetAsync("WeatherForecast", TestContext.Current.CancellationToken);

        // Assert
        Assert.Equal(StatusCodes.Status403Forbidden, (int)response.StatusCode);
    }

    [Fact]
    public async Task Unauthenticated_user_cannot_get_the_forecast()
    {
        // Arrange
        var client = _factory
            .CreateClient();

        // Act
        var response = await client.GetAsync("WeatherForecast", TestContext.Current.CancellationToken);

        // Assert
        Assert.Equal(StatusCodes.Status401Unauthorized, (int)response.StatusCode);
    }
}
