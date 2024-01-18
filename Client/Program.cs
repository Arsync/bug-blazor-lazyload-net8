using LazyTest.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddRemoteAuthentication<RemoteAuthenticationState, RemoteUserAccount,
    OidcProviderOptions>(options =>
{
    options.ProviderOptions.Authority = "https://dummy.keycloak.local/realm/my-realm";

    options.ProviderOptions.ClientId = "blazor-client";
    options.ProviderOptions.DefaultScopes.Add("openid");
    options.ProviderOptions.DefaultScopes.Add("profile");
    options.ProviderOptions.RedirectUri = "https://localhost:9000/authentication/login-callback";
    options.ProviderOptions.PostLogoutRedirectUri = "https://localhost:9000/authentication/logout-callback";
    options.ProviderOptions.ResponseType = "code";
});

builder.Services.AddHttpClient("LazyTest.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("LazyTest.ServerAPI"));

await builder.Build().RunAsync();
