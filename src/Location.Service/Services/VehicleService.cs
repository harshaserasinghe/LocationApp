using Location.Common.Settings;
using Location.Service.Dtos;
using Location.Service.Entities;
using Location.Service.Interfaces;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Location.Service.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly CosmoDBConfig cosmoDBConfig;
        private readonly ICosmosDBService cosmosDBService;

        public VehicleService(IOptions<CosmoDBConfig> cosmoDBConfig,
            ICosmosDBService cosmosDBService)
        {
            this.cosmoDBConfig = cosmoDBConfig.Value;
            this.cosmosDBService = cosmosDBService;
        }

        public async Task<VehicleDto> GetVehicleAsync(string vehicleId)
        {
            try
            {
                var vehicle = await cosmosDBService.GetEntityAsync<Vehicle>(cosmoDBConfig.VehicleContainerId, vehicleId, vehicleId);
                var vehicleDto = new VehicleDto();
                vehicleDto.VehicleId = vehicle.VehicleId;
                vehicleDto.LicenceNo = vehicle.LicenceNo;
                return vehicleDto;
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task AddVehicleAsync(VehicleCreateDto vehicleDto)
        {
            try
            {
                var vehicle = new Vehicle(vehicleDto.VehicleId, vehicleDto.LicenceNo);
                await cosmosDBService.AddEntityAsync(vehicle, cosmoDBConfig.VehicleContainerId);
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.Conflict)
            {
                throw new Exception("This is a registered vehicle");//To do
            }
        }

        public async Task<bool> IsRegisteredAsync(string vehicleId)
        {
            try
            {
                await cosmosDBService.GetEntityAsync<Vehicle>(cosmoDBConfig.VehicleContainerId, vehicleId, vehicleId);
                return true;
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return false;
            }
        }
    }
}
