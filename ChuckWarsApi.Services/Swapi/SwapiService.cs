using ChuckWarsApi.Shared.Dtos.Swapi;
using System.Net.Http.Json;

namespace ChuckWarsApi.Services.Swapi
{
    public class SwapiService : ISwapiService
    {
        private readonly HttpClient _httpClient;

        public SwapiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private const string PeopleEndpoint = "people";
        private const string SearchEndpoint = "people?search={0}";

        public async Task<PeopleDto?> GetPeopleAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<PeopleDto>(PeopleEndpoint);

            return result;
        }

        public async Task<PeopleDto?> SearchPeople(string query)
        {
            var uri = string.Format(SearchEndpoint, query);
            var result = await _httpClient.GetFromJsonAsync<PeopleDto>(uri);
            
            return result;
        }
    }
}
