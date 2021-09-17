﻿using Location.Service.Dtos;
using Location.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public VehiclesController(IVehicleService vehicleService, ILocationService locationService)
        {
            this.vehicleService = vehicleService;
            this.locationService = locationService;
        }

        [HttpGet("{vehicleId}")]
        [Authorize(Policy = "vehicle.policy")]
        [ProducesResponseType(typeof(VehicleDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        public async Task<ActionResult<VehicleDto>> GetVehicleAsync(string vehicleId)
        {
            var vehicleDto = await vehicleService.GetVehicleAsync(vehicleId);
            return Ok(vehicleDto);
        }

        [HttpPost]
        [Authorize(Policy = "vehicle.policy")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        public async Task<ActionResult> AddVehicleAsync([FromBody] VehicleCreateDto VehicleCreateDto)
        {
            await vehicleService.AddVehicleAsync(VehicleCreateDto);
            return Ok();
        }

        [HttpGet("{vehicleId}/current-location")]
        [Authorize(Policy = "admin.policy")]
        [ProducesResponseType(typeof(LocationDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        public async Task<ActionResult<LocationDto>> GetCurrentLocationAsync([FromRoute] string vehicleId)
        {
            var location = await locationService.GetCurrentLocationAsync(vehicleId);
            return Ok(location);
        }

        [HttpGet("{vehicleId}/locations")]
        [Authorize(Policy = "admin.policy")]
        [ProducesResponseType(typeof(List<LocationDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        public async Task<ActionResult<List<LocationDto>>> GetLocationsAsync([FromRoute] string vehicleId, 
            [FromQuery][Required] DateTime fromDateTime, [FromQuery][Required] DateTime toDateTime)
        {
            var locationList = await locationService.GetLocationListAsync(vehicleId, fromDateTime, toDateTime);
            return Ok(locationList);
        }
    }
}
