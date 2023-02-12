using AutoMapper;
using ChuckWarsApi.Services.Chuck;
using ChuckWarsApi.Shared.Models.Chuck;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ChuckWarsApi.Controllers
{
    /// <summary>
    /// Chuck Norris Api Controller
    /// </summary>
    [Route("api/v1/chuck")]
    public class ChuckController : ControllerBase
    {
        private readonly IChuckService _chuckService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        public ChuckController(IChuckService chuckService, IMapper mapper)
        {
            _chuckService = chuckService;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns a list of categories
        /// </summary>
        [Route("categories")]
        [HttpGet]
        public async Task<ChuckCategoriesModel> GetCategories()
        {
            var categories = await _chuckService.GetCategoriesAsync();

            return _mapper.Map<ChuckCategoriesModel>(categories);
        }

        /// <summary>
        /// Returns a random joke based on a catoegory
        /// </summary>
        /// <param name="category">Joke category</param>
        [Route("random-joke/{category}")]
        [HttpGet]
        public async Task<ActionResult<ChuckJokeModel>> GetRandomJoke([Required] string category)
        {
            if (category.Length < 3 || category.Length > 119)
                return BadRequest("Category length should be between 3 and 120 characters");

            var joke = await _chuckService.GetJokeAsync(category);

            return _mapper.Map<ChuckJokeModel>(joke);
        }

        /// <summary>
        /// Search for a joke
        /// </summary>
        /// <param name="query">Search query</param>
        [Route("search/{query}")]
        [HttpGet]
        public async Task<ActionResult<ChuckSearchModel>> SearchJoke([Required] string query)
        {
            if (query.Length < 3 || query.Length > 119)
                return BadRequest("query length should be between 3 and 120 characters");

            var result = await _chuckService.SearchJokeAsync(query);

            return _mapper.Map<ChuckSearchModel>(result);
        }
    }
}
