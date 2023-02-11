using ChuckWarsApi.Shared.Dtos.Chuck;

namespace ChuckWarsApi.Services.Chuck
{
    public interface IChuckService
    {
        public Task<CategoriesDto> GetCategoriesAsync();

        public Task<JokeDto?> GetJokeAsync(string category);

        public Task<SearchDto?> SearchJokeAsync(string query);
    }
}
