//using LMS.Shared.DTOs;
//using Microsoft.AspNetCore.Components;
//using System.Net.Http;
//using System.Text;
//using System.Text.Json;

//namespace LMS.Blazor.Client.Services;

//public class ClientApiService : IApiService
//{
//    private readonly HttpClient httpClient;
//    private readonly NavigationManager navigationManager;
//    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
//    {
//        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
//    };

//    public ClientApiService(IHttpClientFactory httpClientFactory, NavigationManager navigationManager)
//    {
//        httpClient = httpClientFactory.CreateClient("BffClient");
//        this.navigationManager = navigationManager;
//    }

//    /// <summary>
//    /// Makes a GET request to the proxy endpoint and deserializes the response.
//    /// </summary>
//    /// <typeparam name="T">Type of the response DTO.</typeparam>
//    /// <param name="endpoint">The API endpoint to call.</param>
//    /// <returns>A collection of the specified type.</returns>
//    public async Task<T?> CallApiAsync<T>(string endpoint, T payload = default)
//    {
//        HttpRequestMessage requestMessage = payload == null
//            ? new(HttpMethod.Get, $"proxy-endpoint?endpoint={endpoint}")
//            : new(HttpMethod.Post, $"proxy-endpoint?endpoint={endpoint}")
//            {
//                Content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json")
//            };

//        var response = await httpClient.SendAsync(requestMessage);

//        if (response.StatusCode == System.Net.HttpStatusCode.Forbidden ||
//            response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
//        {
//            navigationManager.NavigateTo("AccessDenied");
//            return default;
//        }

//        response.EnsureSuccessStatusCode();

//        var responseData = await JsonSerializer.DeserializeAsync<T>(
//            await response.Content.ReadAsStreamAsync(),
//            _jsonSerializerOptions
//        );

//        return responseData;
//    }

//}



//using LMS.Blazor.Client.Models;
//using LMS.Shared.DTOs;
//using Microsoft.AspNetCore.Components;
//using System.Net.Http;
//using System.Text.Json;

//namespace LMS.Blazor.Client.Services;

//public class ClientApiService(IHttpClientFactory httpClientFactory, NavigationManager navigationManager) : IApiService
//{
//    private readonly HttpClient httpClient = httpClientFactory.CreateClient("BffClient");

//    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
//    { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

//    //ToDo: Make generic
//    public async Task<IEnumerable<DemoDto>> CallApiAsync()
//    {
//        var requestMessage = new HttpRequestMessage(HttpMethod.Get, "proxy-endpoint");
//        var response = await httpClient.SendAsync(requestMessage);

//        if (response.StatusCode == System.Net.HttpStatusCode.Forbidden
//           || response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
//        {
//            navigationManager.NavigateTo("AccessDenied");
//        }

//        response.EnsureSuccessStatusCode();

//        var demoDtos = await JsonSerializer.DeserializeAsync<List<DemoDto>>(await response.Content.ReadAsStreamAsync(), _jsonSerializerOptions, CancellationToken.None) ?? [];
//        return demoDtos;
//    }
//}

//using LMS.Blazor.Client.Models;
//using LMS.Shared.DTOs;
//using Microsoft.AspNetCore.Components;
//using System.Net.Http;
//using System.Net.Http.Headers;
//using System.Net.Http.Json;
//using System.Text.Json;

//namespace LMS.Blazor.Client.Services;

//public class ClientApiService : IApiService
//{

//    private readonly HttpClient httpClient;
//    private readonly NavigationManager navigationManager;
//    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
//    { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

//    public ClientApiService(HttpClient client, NavigationManager navigationManager)
//    {
//        httpClient = client;
//        this.navigationManager = navigationManager;
//        httpClient.BaseAddress = new Uri("https://localhost:7224");
//        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
//    }

//    public async Task<TResponse?> GetAsync<TResponse>(string endpoint)
//    {
//        return await CallApiAsync<object?, TResponse>(
//            endpoint,
//            HttpMethod.Get,
//            null
//        );
//    }

//    public async Task<TResponse?> PostAsync<TRequest, TResponse>(
//        string endpoint,
//        TRequest dto)
//    {
//        return await CallApiAsync<TRequest, TResponse>(
//            endpoint,
//            HttpMethod.Post,
//            dto
//        );
//    }

//    private async Task<TResponse?> CallApiAsync<TRequest, TResponse>(string endpoint, HttpMethod httpMethod, TRequest? dto)
//    {
//        httpClient.BaseAddress = new Uri("https://localhost:7224");
//        var request = new HttpRequestMessage(httpMethod, $"proxy-endpoint/{endpoint}");
//        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

//        if (httpMethod != HttpMethod.Get && dto is not null)
//        {
//            var serialized = JsonSerializer.Serialize(dto);
//            request.Content = new StringContent(serialized);
//            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
//        }

//        var response = await httpClient.SendAsync(request);

//        if (response.StatusCode == System.Net.HttpStatusCode.Forbidden
//           || response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
//        {
//            navigationManager.NavigateTo("AccessDenied");
//        }

//        response.EnsureSuccessStatusCode();

//        var res = await JsonSerializer.DeserializeAsync<TResponse>(await response.Content.ReadAsStreamAsync(), _jsonSerializerOptions, CancellationToken.None);
//        return res;
//    }
//}


// Dimitris

//using LMS.Blazor.Client.Models;
//using LMS.Shared.DTOs;
//using Microsoft.AspNetCore.Components;
//using System.Net.Http;
//using System.Net.Http.Headers;
//using System.Net.Http.Json;
//using System.Text.Json;

//namespace LMS.Blazor.Client.Services;

//public class ClientApiService(IHttpClientFactory httpClientFactory, NavigationManager navigationManager) : IApiService
//{
//    private readonly HttpClient httpClient = httpClientFactory.CreateClient("BffClient");

//    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
//    { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };


//    public async Task<TResponse?> GetAsync<TResponse>(string endpoint)
//    {
//        return await CallApiAsync<object?, TResponse>(
//            endpoint,
//            HttpMethod.Get,
//            null
//        );
//    }

//    public async Task<TResponse?> PostAsync<TRequest, TResponse>(
//        string endpoint,
//        TRequest dto)
//    {
//        return await CallApiAsync<TRequest, TResponse>(
//            endpoint,
//            HttpMethod.Post,
//            dto
//        );
//    }

//    private async Task<TResponse?> CallApiAsync<TRequest, TResponse>(string endpoint, HttpMethod httpMethod, TRequest? dto)
//    {
//        var request = new HttpRequestMessage(httpMethod, $"proxy-endpoint/{endpoint}");
//        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

//        if (httpMethod != HttpMethod.Get && dto is not null)
//        {
//            var serialized = JsonSerializer.Serialize(dto);
//            request.Content = new StringContent(serialized);
//            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
//        }

//        var response = await httpClient.SendAsync(request);

//        if (response.StatusCode == System.Net.HttpStatusCode.Forbidden
//           || response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
//        {
//            navigationManager.NavigateTo("AccessDenied");
//        }

//        response.EnsureSuccessStatusCode();

//        var res = await JsonSerializer.DeserializeAsync<TResponse>(await response.Content.ReadAsStreamAsync(), _jsonSerializerOptions, CancellationToken.None);
//        return res;
//    }
//}

//using LMS.Blazor.Client.Services;
//using Microsoft.AspNetCore.Components;
//using System.Net.Http.Headers;
//using System.Text.Json;

//public class ClientApiService : IApiService
//{
//    private readonly HttpClient httpClient;
//    private readonly NavigationManager navigationManager;
//    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
//    {
//        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
//    };

//    public ClientApiService(HttpClient client, NavigationManager navigationManager)
//    {
//        httpClient = client;
//        this.navigationManager = navigationManager;
//        httpClient.BaseAddress = new Uri("https://localhost:7224");
//        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
//    }

//    public async Task<TResponse?> CallApiAsync<TRequest, TResponse>(string endpoint, HttpMethod method, TRequest? payload = default)
//    {
//        var request = new HttpRequestMessage(method, $"proxy-endpoint/{endpoint}");
//        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

//        if (method != HttpMethod.Get && payload is not null)
//        {
//            var serialized = JsonSerializer.Serialize(payload);
//            request.Content = new StringContent(serialized);
//            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
//        }

//        var response = await httpClient.SendAsync(request);

//        if (response.StatusCode == System.Net.HttpStatusCode.Forbidden ||
//            response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
//        {
//            navigationManager.NavigateTo("AccessDenied");
//        }

//        response.EnsureSuccessStatusCode();

//        return await JsonSerializer.DeserializeAsync<TResponse>(
//            await response.Content.ReadAsStreamAsync(),
//            _jsonSerializerOptions,
//            CancellationToken.None
//        );
//    }
//}


//using LMS.Blazor.Client.Models;
//using LMS.Shared.DTOs;
//using Microsoft.AspNetCore.Components;
//using System.Net.Http;
//using System.Net.Http.Headers;
//using System.Text.Json;

//namespace LMS.Blazor.Client.Services
//{
//    public class ClientApiService : IApiService
//    {
//        private readonly HttpClient httpClient;
//        private readonly NavigationManager navigationManager;
//        private readonly JsonSerializerOptions _jsonSerializerOptions = new()
//        {
//            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
//        };

//        // Constructor for dependency injection
//        public ClientApiService(IHttpClientFactory httpClientFactory, NavigationManager navigationManager)
//        {
//            this.httpClient = httpClientFactory.CreateClient("BffClient");
//            this.navigationManager = navigationManager;

//            if (httpClient.BaseAddress == null)
//            {
//                throw new InvalidOperationException("BaseAddress for HttpClient must be configured.");
//            }

//            this.httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
//        }

//        public async Task<TResponse?> GetAsync<TResponse>(string endpoint)
//        {
//            return await CallApiAsync<object?, TResponse>(endpoint, HttpMethod.Get, null);
//        }

//        public async Task<TResponse?> PostAsync<TRequest, TResponse>(string endpoint, TRequest dto)
//        {
//            return await CallApiAsync<TRequest, TResponse>(endpoint, HttpMethod.Post, dto);
//        }

//        private async Task<TResponse?> CallApiAsync<TRequest, TResponse>(string endpoint, HttpMethod httpMethod, TRequest? dto)
//        {
//            if (string.IsNullOrWhiteSpace(endpoint))
//            {
//                throw new ArgumentException("Endpoint cannot be null or empty.", nameof(endpoint));
//            }

//            // Prepare the HTTP request
//            var request = new HttpRequestMessage(httpMethod, endpoint);
//            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

//            // If it's not a GET request, attach serialized DTO to the request
//            if (httpMethod != HttpMethod.Get && dto is not null)
//            {
//                var serialized = JsonSerializer.Serialize(dto);
//                request.Content = new StringContent(serialized);
//                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
//            }

//            try
//            {
//                // Send the request
//                var response = await httpClient.SendAsync(request);

//                // Handle unauthorized or forbidden responses
//                if (response.StatusCode == System.Net.HttpStatusCode.Forbidden ||
//                    response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
//                {
//                    navigationManager.NavigateTo("AccessDenied");
//                    return default;
//                }

//                response.EnsureSuccessStatusCode();

//                // Deserialize and return the response
//                return await JsonSerializer.DeserializeAsync<TResponse>(
//                    await response.Content.ReadAsStreamAsync(),
//                    _jsonSerializerOptions
//                );
//            }
//            catch (Exception ex)
//            {
//                Console.Error.WriteLine($"Error calling API endpoint '{endpoint}': {ex.Message}");
//                throw;
//            }
//        }
//    }
//}

using LMS.Blazor.Client.Models;
using LMS.Shared.DTOs;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace LMS.Blazor.Client.Services;

public class ClientApiService : IApiService
{
    private readonly HttpClient _httpClient;
    private readonly NavigationManager _navigationManager;
    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public ClientApiService(HttpClient httpClient, NavigationManager navigationManager)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _navigationManager = navigationManager ?? throw new ArgumentNullException(nameof(navigationManager));
    }

    public async Task<TResponse?> GetAsync<TResponse>(string endpoint)
    {
        return await CallApiAsync<object?, TResponse>(endpoint, HttpMethod.Get, null);
    }

    public async Task<TResponse?> PostAsync<TRequest, TResponse>(string endpoint, TRequest dto)
    {
        return await CallApiAsync<TRequest, TResponse>(endpoint, HttpMethod.Post, dto);
    }


    public async Task<TResponse?> PutAsync<TRequest, TResponse>(string endpoint, TRequest dto)
    {
        return await CallApiAsync<TRequest, TResponse>(endpoint, HttpMethod.Put, dto);
    }

    private async Task<TResponse?> CallApiAsync<TRequest, TResponse>(string endpoint, HttpMethod httpMethod, TRequest? dto)
    {
        if (string.IsNullOrWhiteSpace(endpoint))
        {
            throw new ArgumentException("Endpoint cannot be null or empty.", nameof(endpoint));
        }

        var request = new HttpRequestMessage(httpMethod, endpoint);
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        if (httpMethod != HttpMethod.Get && dto is not null)
        {
            var serialized = JsonSerializer.Serialize(dto, _jsonSerializerOptions);
            request.Content = new StringContent(serialized);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        }

        var response = await _httpClient.SendAsync(request);

        if (response.StatusCode == System.Net.HttpStatusCode.Forbidden ||
            response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            _navigationManager.NavigateTo("AccessDenied");
            return default;
        }

        response.EnsureSuccessStatusCode();

        return await JsonSerializer.DeserializeAsync<TResponse>(
            await response.Content.ReadAsStreamAsync(),
            _jsonSerializerOptions
        );
    }
}


//using LMS.Blazor.Client.Services;
//using Microsoft.AspNetCore.Components;
//using System.Net.Http.Headers;
//using System.Text.Json;

//public class ClientApiService : IApiService
//{
//    private readonly HttpClient _httpClient;
//    private readonly NavigationManager _navigationManager;
//    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
//    {
//        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
//    };

//    public ClientApiService(HttpClient httpClient, NavigationManager navigationManager)
//    {
//        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
//        _navigationManager = navigationManager ?? throw new ArgumentNullException(nameof(navigationManager));
//    }

//    public async Task<TResponse?> GetAsync<TResponse>(string endpoint)
//    {
//        return await CallApiAsync<object?, TResponse>(endpoint, HttpMethod.Get, null);
//    }

//    public async Task<TResponse?> PostAsync<TRequest, TResponse>(string endpoint, TRequest dto)
//    {
//        return await CallApiAsync<TRequest, TResponse>(endpoint, HttpMethod.Post, dto);
//    }

//    public async Task<TResponse?> PutAsync<TRequest, TResponse>(string endpoint, TRequest dto)
//    {
//        return await CallApiAsync<TRequest, TResponse>(endpoint, HttpMethod.Put, dto);
//    }

//    private async Task<TResponse?> CallApiAsync<TRequest, TResponse>(string endpoint, HttpMethod httpMethod, TRequest? dto)
//    {
//        if (string.IsNullOrWhiteSpace(endpoint))
//        {
//            throw new ArgumentException("Endpoint cannot be null or empty.", nameof(endpoint));
//        }

//        var request = new HttpRequestMessage(httpMethod, endpoint);
//        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

//        if (httpMethod != HttpMethod.Get && dto is not null)
//        {
//            var serialized = JsonSerializer.Serialize(dto, _jsonSerializerOptions);
//            request.Content = new StringContent(serialized);
//            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
//        }

//        var response = await _httpClient.SendAsync(request);

//        if (response.StatusCode == System.Net.HttpStatusCode.Forbidden ||
//            response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
//        {
//            _navigationManager.NavigateTo("AccessDenied");
//            return default;
//        }

//        response.EnsureSuccessStatusCode();

//        return await JsonSerializer.DeserializeAsync<TResponse>(
//            await response.Content.ReadAsStreamAsync(),
//            _jsonSerializerOptions
//        );
//    }
//}
