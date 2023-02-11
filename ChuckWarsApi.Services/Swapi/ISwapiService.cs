using ChuckWarsApi.Shared.Dtos.Swapi;

namespace ChuckWarsApi.Services.Swapi
{
    public interface ISwapiService
    {
        public Task<PeopleDto?> GetPeopleAsync();

        public Task<PeopleDto?> SearchPeople(string query);
    }
}
