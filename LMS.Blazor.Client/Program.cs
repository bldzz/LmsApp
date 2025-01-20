using LMS.Blazor.Client.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Register API service for calls originating from WASM
builder.Services.AddScoped<IApiService, ClientApiService>();
builder.Services.AddHttpClient("BffClient", cfg =>
{
    cfg.BaseAddress = new Uri("https://localhost:7224");
    //cfg.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress); // Dynamic base address
});

builder.Services.AddSingleton<AuthenticationStateProvider,
    PersistentAuthenticationStateProvider>();
builder.Services.AddCascadingAuthenticationState();

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
