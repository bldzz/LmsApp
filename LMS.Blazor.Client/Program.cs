using LMS.Blazor.Client.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Register API service for calls originating from WASM
builder.Services.AddHttpClient<IApiService, ClientApiService>("BffClient", cfg =>
{
    cfg.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress); // Dynamic base address
});

// Scoped AuthenticationStateProvider
builder.Services.AddScoped<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();

// Add Blazor authorization services
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();

try
{
    await builder.Build().RunAsync();
}
catch (Exception ex)
{
    await Console.Error.WriteLineAsync($"An error occurred during startup: {ex.Message}");
    throw;
}
