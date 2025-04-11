namespace TestAuthentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Security.Claims;

public class TestAuthenticationWebApplicationFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint> where TEntryPoint : class
{
    public WebApplicationFactory<TEntryPoint> WithTestSchemeAuth(IReadOnlyList<Claim> claims)
    {
        return WithWebHostBuilder(builder => builder.ConfigureTestServices(
               services =>
               {
                   services.AddAuthentication(TestAuthenticationSchemeOptions.DefaultScheme)
                   .AddTestAuthentication(options =>
                   {
                       options.Claims = claims;
                   });
               }
           ));
    }
}
