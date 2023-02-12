using ChuckWarsApi.Shared.Dtos.Swapi;

namespace ChuckWarsApi.Services.Swapi
{
    public interface ISwapiService
    {
        public Task<PeopleDto?> GetPeopleAsync(int pageNumber);

        public Task<PeopleDto?> SearchPeople(string query);
    }
}
