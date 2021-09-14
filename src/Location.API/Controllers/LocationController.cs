using Location.Service.Dtos;
using Location.Service.Interfaces;
using Location.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Location.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService locationService;
        public LocationController(ILocationService locationService)
        {
            this.locationService = locationService;
        }

        [Route("GetLocation")]
        [HttpGet]
        public async Task<IActionResult> GetAsync(string vehcileId)
        {
            var location = await locationService.GetLocationAsync(vehcileId);
            return Ok(location);
        }

        [Route("GetLocationList")]
        [HttpGet]
        public async Task<IActionResult> GetAsync(string vehicleId, DateTime fromDateTime, DateTime toDateTime)
        {
            var locationList = await locationService.GetLocationListAsync(vehicleId, fromDateTime, toDateTime);
            return Ok(locationList);
        }

        [Route("AddLocation")]
        [HttpPost]
        public async Task<IActionResult> PostAsync(LocationCreateDto locationDto)
        {
            await locationService.AddLocationAsync(locationDto);
            return Ok();
        }
    }
}
