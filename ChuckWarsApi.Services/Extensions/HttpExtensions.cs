using ChuckWarsApi.Shared.Models;
using System.Net.Http.Json;

namespace ChuckWarsApi.Services.Extensions
{
    internal static class HttpExtensions
    {
        public static async Task<ServiceResponseModel<T>> GetResultAsync<T>(this HttpResponseMessage message)
            where T : class
        {
            if (!message.IsSuccessStatusCode)
            {
                var errorMessage = await message.Content.ReadAsStringAsync();

                return new ServiceResponseModel<T>
                {
                    IsSuccessful = false,
                    ErrorMessage = errorMessage,
                    Data = null
                };
            }

            var data = await message.Content.ReadFromJsonAsync<T>();

            return new ServiceResponseModel<T>
            {
                IsSuccessful = true,
                Data = data
            };
        }
    }
}
