namespace Weather.Tests;
using TestAuthentication;

[CollectionDefinition("API collection")]
public class ApiCollectionFixture : ICollectionFixture<TestWebApplicationFactory<Program>>
{
}
