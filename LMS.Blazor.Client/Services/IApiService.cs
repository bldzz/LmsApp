using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMS.Blazor.Client.Services
{
    public interface IApiService
    {
        /// <summary>
        /// Calls an API endpoint and returns a collection of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the objects in the returned collection.</typeparam>
        /// <param name="endpoint">The API endpoint to call.</param>
        /// <returns>A task representing the asynchronous operation, containing a collection of the specified type.</returns>
        Task<T> CallApiAsync<T>(string endpoint, T payload = default);
    }
}
