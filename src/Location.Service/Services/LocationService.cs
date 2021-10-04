using AutoMapper;
using Location.Common.Settings;
using Location.Core.Dtos;
using Location.Core.Interfaces;
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
        private readonly IMapper mapper;
        private readonly ICosmosDBService cosmosDBService;
        private readonly IVehicleService vehicleService;
        private readonly IMapService mapService;

        public LocationService(IOptions<CosmoDBConfig> cosmoDBConfig,
            IMapper mapper,
            ICosmosDBService cosmosDBService,
            IVehicleService vehicleService,
            IMapService mapService)
        {
            this.cosmoDBConfig = cosmoDBConfig.Value;
            this.mapper = mapper;
            this.cosmosDBService = cosmosDBService;
            this.vehicleService = vehicleService;
            this.mapService = mapService;
        }

        public async Task AddLocationAsync(LocationCreateDto locationCreateDto)
        {
            if (!await vehicleService.IsRegisteredAsync(locationCreateDto.VehicleId))
                throw new Common.Exceptions.ApplicationException((int)HttpStatusCode.BadRequest, "Vehicle is not registered");

            var location = mapper.Map<Core.Entities.Location>(locationCreateDto);

            await cosmosDBService.AddEntityAsync(location, cosmoDBConfig.LocationContainerId);
            location.UpdateId();
            await cosmosDBService.UpdateEntityAsync(location, cosmoDBConfig.CurrentLocationContainerId, location.VehicleId);
        }

        public async Task<LocationDto> GetCurrentLocationAsync(string vehicleId)
        {
            if (!await vehicleService.IsRegisteredAsync(vehicleId))
                throw new Common.Exceptions.ApplicationException((int)HttpStatusCode.BadRequest, "Vehicle is not registered");

            try
            {
                var location = await cosmosDBService.GetEntityAsync<Core.Entities.Location>(cosmoDBConfig.CurrentLocationContainerId, vehicleId, vehicleId);
                var locationDto = mapper.Map<LocationDto>(location);
                var locality = await mapService.GetLocality(locationDto.Latitude, locationDto.Longitude);
                locationDto.SetLocality(locality);
                return locationDto;
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                throw new Common.Exceptions.ApplicationException((int)HttpStatusCode.NotFound, "Location data is not available");
            }
        }

        public async Task<List<LocationDto>> GetLocationListAsync(string vehicleId, DateTime fromDateTime, DateTime toDateTime)
        {
            if (!await vehicleService.IsRegisteredAsync(vehicleId))
                throw new Common.Exceptions.ApplicationException((int)HttpStatusCode.BadRequest, "Vehicle is not registered");

            var query = $"select * from location " +
                $"where location.VehicleId = '{vehicleId}' and " +
                $"location.CreatedDate >= '{fromDateTime.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ")}' and " +
                $"location.CreatedDate <= '{toDateTime.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ")}'";

            var locationList = await cosmosDBService.GetEntitiesAsync<Core.Entities.Location>(cosmoDBConfig.LocationContainerId, query);
            var locationDtoList = mapper.Map<List<LocationDto>>(locationList);
            return locationDtoList;
        }
    }
}
