# TestAuthentication

Creating a Test Authentication Scheme can streamline API testing for scenarios where Authorization is enabled. By setting up a test authentication mechanism, you can bypass actual authentication processes and simulate various user identities, claims, or roles. This approach allows you to focus on testing the functionality of your API endpoints with authorization enabled.


Example of a Test Authentication Scheme

```csharp

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
      var response = await client.GetAsync("WeatherForecast");

      // Assert
      response.EnsureSuccessStatusCode(); // Status Code 200-299
      var weather = await response.Content.ReadAsStringAsync();
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
      var response = await client.GetAsync("WeatherForecast");

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
      var response = await client.GetAsync("WeatherForecast");

      // Assert
      Assert.Equal(StatusCodes.Status401Unauthorized, (int)response.StatusCode);
  }
```