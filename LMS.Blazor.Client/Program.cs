using LMS.Blazor.Client.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Register API service for calls originating from WASM
builder.Services.AddHttpClient<IApiService, ClientApiService>("BffClient", cfg =>
{
    // Ensure the base address for HTTP calls is set correctly
    cfg.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress); // Dynamic base address
});

// Register the AuthenticationStateProvider for scoped use
builder.Services.AddScoped<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();

// Add authorization services to enable role and policy-based access control
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();

// Build and run the application
try
{
    await builder.Build().RunAsync();
}
catch (Exception ex)
{
    Console.Error.WriteLine($"An error occurred during startup: {ex.Message}");
    throw;
}



//using LMS.Blazor.Client.Services;
//using Microsoft.AspNetCore.Components.Authorization;
//using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

//var builder = WebAssemblyHostBuilder.CreateDefault(args);

//// Register API service for calls originating from WASM
//builder.Services.AddHttpClient<IApiService, ClientApiService>("BffClient", cfg =>
//{
//    cfg.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress); // Dynamic base address
//});

//// Scoped AuthenticationStateProvider
//builder.Services.AddScoped<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();

//// Add Blazor authorization services
//builder.Services.AddOptions();
//builder.Services.AddAuthorizationCore();

//try
//{
//    await builder.Build().RunAsync();
//}
//catch (Exception ex)
//{
//    Console.Error.WriteLine($"An error occurred during startup: {ex.Message}");
//    throw;
//}
