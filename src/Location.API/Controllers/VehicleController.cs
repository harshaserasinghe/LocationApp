using Location.Service.Dtos;
using Location.Service.Entities;
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
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService vehicleService;
        public VehicleController(IVehicleService vehicleService)
        {
            this.vehicleService = vehicleService;
        }

        [Route("GetVehicle")]
        [HttpGet]
        public async Task<IActionResult> GetAsync(string vehcileId)
        {
            var vehicle = await vehicleService.GetVehicleAsync(vehcileId);
            return Ok(vehicle);
        }

        [Route("AddVehicle")]
        [HttpPost]
        public async Task<IActionResult> PostAsync(VehicleCreateDto vehicleDto)
        {
            await vehicleService.AddVehicleAsync(vehicleDto);
            return Ok();
        }
    }
}
