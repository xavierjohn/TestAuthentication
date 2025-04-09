namespace TestAuthentication;
using Microsoft.AspNetCore.Authentication;

public static class AuthenticationBuilderExtensions
{
    public static AuthenticationBuilder AddTestAuthentication(this AuthenticationBuilder builder, Action<TestAuthenticationSchemeOptions> configureOptions)
        => builder.AddScheme<TestAuthenticationSchemeOptions, TestAuthenticationHandler>(TestAuthenticationSchemeOptions.DefaultScheme, configureOptions);
}
