using AutoMapper;
using ChuckWarsApi.Services.Swapi;
using ChuckWarsApi.Shared.Models.Swapi;
using Microsoft.AspNetCore.Mvc;

namespace ChuckWarsApi.Controllers
{
    /// <summary>
    /// Star Wars API Controller
    /// </summary>
    [Route("api/v1/swapi")]
    public class SwapiController : ControllerBase
    {
        private readonly ISwapiService _swapiService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        public SwapiController(ISwapiService swapiService, IMapper mapper)
        {
            _swapiService = swapiService;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets a list of Star Wars People
        /// </summary>
        [HttpGet("people")]
        public async Task<PeopleModel> GetPeople()
        {
            var result = await _swapiService.GetPeopleAsync();

            return _mapper.Map<PeopleModel>(result);
        }
    }
}
