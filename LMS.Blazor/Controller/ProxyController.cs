using LMS.Blazor.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Security.Claims;

//namespace LMS.Blazor.Controller;

[Route("proxy-endpoint")]
[ApiController]
public class ProxyController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ITokenStorage _tokenService;

    public ProxyController(IHttpClientFactory httpClientFactory, ITokenStorage tokenService)
    {
        _httpClientFactory = httpClientFactory;
        _tokenService = tokenService;
    }


    [Route("{*endpoint}")]
    [Authorize]
    public async Task<IActionResult> Proxy(string endpoint)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; //Usermanager can be used here! 

        if (userId == null)
            return Unauthorized();


        var accessToken = await _tokenService.GetAccessTokenAsync(userId);

        //ToDo: Before continue look for expired accesstoken and call refresh enpoint instead.
        //Better with delegatinghandler or separate service to extract this logic!

        if (string.IsNullOrEmpty(accessToken))
        {
            return Unauthorized();
        }
        var client = _httpClientFactory.CreateClient("LmsAPIClient");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        var targetUri = new Uri($"{client.BaseAddress}{endpoint}{Request.QueryString}");
        var method = new HttpMethod(Request.Method);
        var requestMessage = new HttpRequestMessage(method, targetUri);

        if (method != HttpMethod.Get && Request.ContentLength > 0)
        {

            requestMessage.Content = new StreamContent(Request.Body);

            if (!string.IsNullOrWhiteSpace(Request.ContentType))
            {
                requestMessage.Content.Headers.ContentType
                    = MediaTypeHeaderValue.Parse(Request.ContentType);
            }
        }

        foreach (var header in Request.Headers)
        {
            if (!header.Key.Equals("Host", StringComparison.OrdinalIgnoreCase))
            {
                requestMessage.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
            }
        }

        var response = await client.SendAsync(requestMessage);

        if (!response.IsSuccessStatusCode)
            return Unauthorized(); //ToDo pass correct statuscode to caller

        Response.StatusCode = (int)response.StatusCode;
        Response.ContentType = response.Content.Headers.ContentType?.ToString() ?? "application/json";

        var stream = await response.Content.ReadAsStreamAsync();
        await stream.CopyToAsync(Response.Body);

        return new EmptyResult();

    }
}



//using LMS.Blazor.Services;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System.Net.Http.Headers;
//using System.Security.Claims;

//namespace LMS.Blazor.Controller;

//[Route("proxy-endpoint")]
//[ApiController]
//public class ProxyController : ControllerBase
//{
//    private readonly IHttpClientFactory _httpClientFactory;
//    private readonly ITokenStorage _tokenService;

//    public ProxyController(IHttpClientFactory httpClientFactory, ITokenStorage tokenService)
//    {
//        _httpClientFactory = httpClientFactory;
//        _tokenService = tokenService;
//    }

//    [HttpGet]
//    //[HttpPost]
//    //[HttpPut]
//    //[HttpDelete]
//    //[HttpPatch]
//    public async Task<IActionResult> Proxy() //ToDo send endpoint uri here!
//    {
//        string endpoint = "api/demoauth";
//        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

//        if (userId == null)
//            return Unauthorized();


//        var accessToken = await _tokenService.GetAccessTokenAsync(userId);

//        //ToDo: Before continue look for expired accesstoken and call refresh enpoint instead.
//        //Better with delegatinghandler or separate service to extract this logic!

//        if (string.IsNullOrEmpty(accessToken))
//        {
//            return Unauthorized();
//        }
//        var client = _httpClientFactory.CreateClient("LmsAPIClient");
//        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

//        var targetUri = new Uri($"{client.BaseAddress}{endpoint}{Request.QueryString}");
//        var method = new HttpMethod(Request.Method);
//        var requestMessage = new HttpRequestMessage(method, targetUri);

//        if (method != HttpMethod.Get && Request.ContentLength > 0)
//        {
//            requestMessage.Content = new StreamContent(Request.Body);
//        }

//        foreach (var header in Request.Headers)
//        {
//            if (!header.Key.Equals("Host", StringComparison.OrdinalIgnoreCase))
//            {
//                requestMessage.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
//            }
//        }


//        var response = await client.SendAsync(requestMessage);

//        if (!response.IsSuccessStatusCode)
//            return Unauthorized();

//        return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
//    }
//}


//using Azure.Core;
//using Azure;
//using LMS.Blazor.Services;
//using Microsoft.AspNetCore.Mvc;
//using System.Net.Http.Headers;
//using System.Security.Claims;

//[Route("proxy-endpoint")]
//[ApiController]
//public class ProxyController : ControllerBase
//{
//    private readonly IHttpClientFactory _httpClientFactory;
//    private readonly ITokenStorage _tokenService;
//    private readonly ILogger<ProxyController> _logger;

//    public ProxyController(IHttpClientFactory httpClientFactory, ITokenStorage tokenService, ILogger<ProxyController> logger)
//    {
//        _httpClientFactory = httpClientFactory;
//        _tokenService = tokenService;
//        _logger = logger;
//    }

//    [HttpGet, HttpPost, HttpPut, HttpDelete, HttpPatch]
//    public async Task<IActionResult> Proxy([FromQuery] string endpoint)
//    {
//        if (string.IsNullOrEmpty(endpoint) || !IsAllowedEndpoint(endpoint))
//            return BadRequest("Invalid or forbidden endpoint.");

//        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
//        if (userId == null) return Unauthorized();

//        var accessToken = await _tokenService.GetAccessTokenAsync(userId);



//        if (await _tokenService.IsTokenExpiredAsync(userId))
//        {
//            var refreshToken = await _tokenService.GetRefreshTokenAsync(userId);
//            accessToken = await _tokenService.RefreshAccessTokenAsync(refreshToken);
//            if (string.IsNullOrEmpty(accessToken)) return Unauthorized();
//        }




//        var client = _httpClientFactory.CreateClient("LmsAPIClient");
//        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

//        var targetUri = new Uri($"{client.BaseAddress}{endpoint}{Request.QueryString}");
//        var requestMessage = new HttpRequestMessage(new HttpMethod(Request.Method), targetUri);

//        if (Request.Method != HttpMethod.Get.Method && Request.ContentLength > 0)
//            requestMessage.Content = new StreamContent(Request.Body);

//        foreach (var header in Request.Headers)
//        {
//            if (!header.Key.Equals("Host", StringComparison.OrdinalIgnoreCase))
//                requestMessage.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
//        }

//        try
//        {
//            var response = await client.SendAsync(requestMessage);
//            foreach (var header in response.Headers)
//                Response.Headers[header.Key] = string.Join(", ", header.Value);

//            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
//        }
//        catch (Exception ex)
//        {
//            _logger.LogError(ex, "Error while proxying request to {Uri}", targetUri);
//            return StatusCode(500, "Internal Server Error");
//        }
//    }

//    private bool IsAllowedEndpoint(string endpoint)
//    {
//        var allowedEndpoints = new[] { "api/demoauth", "api/users", "api/courses" };
//        return allowedEndpoints.Contains(endpoint);
//    }
//}
