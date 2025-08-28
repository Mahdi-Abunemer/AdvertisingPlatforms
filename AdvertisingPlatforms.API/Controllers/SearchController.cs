using AdvertisingPlatforms.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AdvertisingPlatforms.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IPlatformSearchService _search;

        public SearchController(IPlatformSearchService search)
        {
            _search = search;
        }

        [HttpGet]
        [Route("[Action]")]
        public IActionResult Get([FromQuery] string location)
        {
            var platforms = _search.FindByLocation(location);

            var result = platforms.Select(p => new
            {
                name = p.Name,
                locations = p.Locations?.Select(l => l.Value).ToArray() ?? new string[0]
            });

            return Ok(result);
        }
    }
}
