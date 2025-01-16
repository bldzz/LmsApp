using LMS.Shared.DTOs;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace LMS.Blazor.Client.Services;

public class ClientApiService : IApiService
{
    private readonly HttpClient httpClient;
    private readonly NavigationManager navigationManager;
    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public ClientApiService(IHttpClientFactory httpClientFactory, NavigationManager navigationManager)
    {
        httpClient = httpClientFactory.CreateClient("BffClient");
        this.navigationManager = navigationManager;
    }

    /// <summary>
    /// Makes a GET request to the proxy endpoint and deserializes the response.
    /// </summary>
    /// <typeparam name="T">Type of the response DTO.</typeparam>
    /// <param name="endpoint">The API endpoint to call.</param>
    /// <returns>A collection of the specified type.</returns>
    public async Task<T?> CallApiAsync<T>(string endpoint, T payload = default)
    {
        HttpRequestMessage requestMessage = payload == null
            ? new(HttpMethod.Get, $"proxy-endpoint?endpoint={endpoint}")
            : new(HttpMethod.Post, $"proxy-endpoint?endpoint={endpoint}")
            {
                Content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json")
            };

        var response = await httpClient.SendAsync(requestMessage);

        if (response.StatusCode == System.Net.HttpStatusCode.Forbidden ||
            response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            navigationManager.NavigateTo("AccessDenied");
            return default;
        }

        response.EnsureSuccessStatusCode();

        var responseData = await JsonSerializer.DeserializeAsync<T>(
            await response.Content.ReadAsStreamAsync(),
            _jsonSerializerOptions
        );

        return responseData;
    }

}



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

