﻿using Location.Service.Dtos;
using Location.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Location.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleService vehicleService;
        private readonly ILocationService locationService;
        public VehiclesController(IVehicleService vehicleService,ILocationService locationService)
        {
            this.vehicleService = vehicleService;
            this.locationService = locationService;
        }

        [HttpGet("{vehicleId}")]
        [ProducesResponseType(typeof(VehicleDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<VehicleDto>> GetVehicleAsync(string vehicleId)
        {
            var vehicle = await vehicleService.GetVehicleAsync(vehicleId);
            return Ok(vehicle);
        }

        [HttpGet("{vehicleId}/current-location")]
        [ProducesResponseType(typeof(Service.Entities.Location), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<Service.Entities.Location>> GetCurrentLocationAsync([FromRoute] string vehicleId)
        {
            var location = await locationService.GetCurrentLocationAsync(vehicleId);
            return Ok(location);
        }

        [HttpGet("{vehicleId}/locations")]
        [ProducesResponseType(typeof(List<Service.Entities.Location>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<Service.Entities.Location>>> GetLocationsAsync([FromRoute]string vehicleId, [FromQuery] DateTime fromDateTime, [FromQuery] DateTime toDateTime)
        {
            var locationList = await locationService.GetLocationListAsync(vehicleId, fromDateTime, toDateTime);
            return Ok(locationList);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> AddVehicleAsync([FromBody]VehicleCreateDto vehicleDto)
        {
            await vehicleService.AddVehicleAsync(vehicleDto);
            return Ok();
        }
    }
}
