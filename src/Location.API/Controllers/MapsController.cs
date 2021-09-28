using Location.Core.Dtos.Google;
using Location.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;

namespace Location.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MapsController : ControllerBase
    {
        private readonly IMapService mapService;
        public MapsController(IMapService mapService)
        {
            this.mapService = mapService;
        }

        [HttpGet()]
        [ProducesResponseType(typeof(MapDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<MapDto>> GetMapDataAsync([FromQuery][Required] double latitude, [FromQuery][Required] double longitude)
        {
            var mapDto = await mapService.GetMapDataAsync(latitude, longitude);
            return Ok(mapDto);
        }
    }
}
