using ChuckWarsApi.Shared.Models.Chuck;
using ChuckWarsApi.Shared.Models.Swapi;

namespace ChuckWarsApi.Shared.Models.Search
{
    public class SearchResultModel
    {
        public ChuckSearchModel? ChuckNorrisResults { get; set; }

        public PeopleModel? SwApiResults { get; set; }
    }
}
