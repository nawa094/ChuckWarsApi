using ChuckWarsApi.Shared.Dtos.Chuck;
using System.Net.Http.Json;

namespace ChuckWarsApi.Services.Chuck
{
    public class ChuckService : IChuckService
    {
        private readonly HttpClient _httpClient;

        public ChuckService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private const string CategoriesEndpoint = "categories";
        private const string RandomJokeEndpoint = "random?category={0}";
        private const string SearchEndpoint = "search?query={0}";

        public async Task<CategoriesDto> GetCategoriesAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<string>>(CategoriesEndpoint) ?? new List<string>();

            return new CategoriesDto
            {
                Categories = response
            };
        }

        public async Task<JokeDto?> GetJokeAsync(string category)
        {
            var uri = string.Format(RandomJokeEndpoint, category);

            return await _httpClient.GetFromJsonAsync<JokeDto>(uri);
        }

        public async Task<SearchDto?> SearchJokeAsync(string query)
        {
            if (query.Length < 3 || query.Length > 120)
                return null;

            var uri = string.Format(SearchEndpoint, query);

            var result = await _httpClient.GetFromJsonAsync<SearchDto>(uri);
            return result;
        }
    }
}
