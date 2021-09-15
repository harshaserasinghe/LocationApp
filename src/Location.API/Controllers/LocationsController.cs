using Location.Service.Dtos;
using Location.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace Location.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationService locationService;
        public LocationsController(ILocationService locationService)
        {
            this.locationService = locationService;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddLocationAsync([FromBody] LocationCreateDto locationCreateDto)
        {
            await locationService.AddLocationAsync(locationCreateDto);
            return Ok();
        }
    }
}
