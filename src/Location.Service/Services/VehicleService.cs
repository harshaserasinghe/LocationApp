using AutoMapper;
using Location.Common.Exceptions;
using Location.Common.Settings;
using Location.Service.Dtos;
using Location.Service.Entities;
using Location.Service.Interfaces;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;
using System.Net;
using System.Threading.Tasks;

namespace Location.Service.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly CosmoDBConfig cosmoDBConfig;
        private readonly ICosmosDBService cosmosDBService;
        private readonly IMapper mapper;

        public VehicleService(IOptions<CosmoDBConfig> cosmoDBConfig,
            IMapper mapper,
            ICosmosDBService cosmosDBService)
        {
            this.cosmoDBConfig = cosmoDBConfig.Value;
            this.mapper = mapper;
            this.cosmosDBService = cosmosDBService;
        }

        public async Task<VehicleDto> GetVehicleAsync(string vehicleId)
        {
            try
            {
                var vehicle = await cosmosDBService.GetEntityAsync<Vehicle>(cosmoDBConfig.VehicleContainerId, vehicleId, vehicleId);
                var vehicleDto = mapper.Map<VehicleDto>(vehicle);
                return vehicleDto;
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                throw new ApplicationException((int)HttpStatusCode.NotFound, "Vehicle is not registered");
            }
        }

        public async Task AddVehicleAsync(VehicleCreateDto VehicleCreateDto)
        {
            try
            {
                var vehicle = mapper.Map<Vehicle>(VehicleCreateDto);
                await cosmosDBService.AddEntityAsync(vehicle, cosmoDBConfig.VehicleContainerId);
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.Conflict)
            {
                throw new ApplicationException((int)HttpStatusCode.BadRequest, "Vehicle is already registered");
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
