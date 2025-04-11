namespace Weather.Tests;
using TestAuthentication;

[CollectionDefinition(Id)]
public class ApiCollectionFixture : ICollectionFixture<TestAuthenticationWebApplicationFactory<Program>>
{
    public const string Id = "Test web application factory fixture collection";
}
