//using LMS.Blazor.Client.Services;
//using Microsoft.AspNetCore.Components.Authorization;
//using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

//var builder = WebAssemblyHostBuilder.CreateDefault(args);

//// Register API service for calls originating from WASM
//builder.Services.AddHttpClient<IApiService, ClientApiService>("BffClient", cfg =>
//{
//    // Ensure the base address for HTTP calls is set correctly
//    cfg.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress); // Dynamic base address
//});

//// Register the AuthenticationStateProvider for scoped use
//builder.Services.AddScoped<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();

//// Add authorization services to enable role and policy-based access control
//builder.Services.AddOptions();
//builder.Services.AddAuthorizationCore();

//// Build and run the application
//try
//{
//    await builder.Build().RunAsync();
//}
//catch (Exception ex)
//{
//    Console.Error.WriteLine($"An error occurred during startup: {ex.Message}");
//    throw;
//}



using LMS.Blazor.Client.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Configure LocalAPIBaseAddress from appsettings.json
var localApiBaseAddress = builder.Configuration["LocalAPIBaseAddress"];
if (string.IsNullOrEmpty(localApiBaseAddress))
{
    throw new InvalidOperationException("LocalAPIBaseAddress is not configured in appsettings.json.");
}

// Register API service for calls originating from WASM
builder.Services.AddHttpClient<IApiService, ClientApiService>(client =>
{
    // Set the base address for HTTP calls
    client.BaseAddress = new Uri(localApiBaseAddress);
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

