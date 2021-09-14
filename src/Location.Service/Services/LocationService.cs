using Location.Common.Settings;
using Location.Service.Dtos;
using Location.Service.Interfaces;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Location.Service.Services
{
    public class LocationService : ILocationService
    {
        private readonly CosmoDBConfig cosmoDBConfig;
        private readonly ICosmosDBService cosmosDBService;
        private readonly IVehicleService vehicleService;

        public LocationService(IOptions<CosmoDBConfig> cosmoDBConfig,
            ICosmosDBService cosmosDBService,
            IVehicleService vehicleService)
        {
            this.cosmoDBConfig = cosmoDBConfig.Value;
            this.cosmosDBService = cosmosDBService;
            this.vehicleService = vehicleService;
        }

        public async Task AddLocationAsync(LocationCreateDto locationDto)
        {
            if (!await vehicleService.IsRegisteredAsync(locationDto.VehicleId))
                throw new Exception("This is not a registered vehicle"); //To do

            var location = new Entities.Location(locationDto.VehicleId, locationDto.Latitude, locationDto.Longitude, locationDto.CreatedDate);

            await cosmosDBService.AddEntityAsync(location, cosmoDBConfig.LocationContainerId);
            location.UpdateId();
            await cosmosDBService.UpdateEntityAsync(location, cosmoDBConfig.CurrentLocationContainerId, location.VehicleId);
            //try
            //{
            //    await cosmosDBService.UpdateEntityAsync(location, cosmoDBConfig.CurrentLocationContainerId, location.VehicleId, location.VehicleId);
            //}
            //catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            //{
            //    await cosmosDBService.AddEntityAsync(location,cosmoDBConfig.CurrentLocationContainerId);
            //}           
        }

        public async Task<Entities.Location> GetCurrentLocationAsync(string vehicleId)
        {
            if (!await vehicleService.IsRegisteredAsync(vehicleId))
                throw new Exception("This is not a registered vehicle");//To do

            //var query = $"select * from location where location.VehicleId = '{vehicleId}'";
            //var location = (await cosmosDBService.GetEntitiesAsync<Entities.Location>(cosmoDBConfig.CurrentLocationContainerId, query)).FirstOrDefault();
            //return location;

            try
            {
                var location = await cosmosDBService.GetEntityAsync<Entities.Location>(cosmoDBConfig.CurrentLocationContainerId, vehicleId, vehicleId);
                return location;
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<List<Entities.Location>> GetLocationListAsync(string vehicleId, DateTime fromDateTime, DateTime toDateTime)
        {
            if (!await vehicleService.IsRegisteredAsync(vehicleId))
                throw new Exception("This is not a registered vehicle");//To do

            var query = $"select * from location " +
                $"where location.VehicleId = '{vehicleId}' and " +
                $"location.CreatedDate >= '{fromDateTime.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ")}' and " +
                $"location.CreatedDate <= '{toDateTime.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ")}'";

            var locationList = await cosmosDBService.GetEntitiesAsync<Entities.Location>(cosmoDBConfig.LocationContainerId, query);
            return locationList;
        }
    }
}
