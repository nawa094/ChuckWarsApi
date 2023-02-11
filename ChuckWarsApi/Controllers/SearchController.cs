using AutoMapper;
using ChuckWarsApi.Services.Chuck;
using ChuckWarsApi.Services.Swapi;
using ChuckWarsApi.Shared.Models.Chuck;
using ChuckWarsApi.Shared.Models.Search;
using ChuckWarsApi.Shared.Models.Swapi;
using Microsoft.AspNetCore.Mvc;

namespace ChuckWarsApi.Controllers
{
    /// <summary>
    /// Search Chuck Norris and Star Wars Api simultaneously
    /// </summary>
    [Route("api/v1/search")]
    public class SearchController : ControllerBase
    {
        private readonly IChuckService _chuckService;
        private readonly ISwapiService _swapiService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        public SearchController(ISwapiService swapiService,
            IChuckService chuckService,
            IMapper mapper)
        {
            _swapiService = swapiService;
            _chuckService = chuckService;
            _mapper = mapper;
        }

        /// <summary>
        /// Search method
        /// </summary>
        [HttpGet("{query}")]
        public async Task<ActionResult<SearchResultModel>> Search(string query)
        {
            var chuckSearchTask = _chuckService.SearchJokeAsync(query);
            var swapiSearchTask = _swapiService.SearchPeople(query);

            await Task.WhenAll(new Task[] { swapiSearchTask, chuckSearchTask });

            var (chuckSearchResult, swapiSearchResult) = (chuckSearchTask.Result, swapiSearchTask.Result);

            return new SearchResultModel
            {
               ChuckNorrisResults = _mapper.Map<ChuckSearchModel>(chuckSearchResult),
               SwApiResults = _mapper.Map<PeopleModel>(swapiSearchResult)
            };
        }
    }
}
