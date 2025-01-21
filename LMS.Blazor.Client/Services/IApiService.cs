namespace LMS.Blazor.Client.Services
{
    public interface IApiService
    {
        /// <summary>
        /// Calls an API endpoint and returns a response of the specified type.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request payload.</typeparam>
        /// <typeparam name="TResponse">The type of the response payload.</typeparam>
        /// <param name="endpoint">The API endpoint to call.</param>
        /// <param name="payload">The request payload (optional for GET requests).</param>
        /// <returns>A task containing the response payload.</returns>
        Task<TResponse?> CallApiAsync<TRequest, TResponse>(string endpoint, HttpMethod method, TRequest? payload = default);
    }
}
