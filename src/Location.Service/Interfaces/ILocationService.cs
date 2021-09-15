using Location.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Location.Service.Interfaces
{
    public interface ILocationService
    {
        Task AddLocationAsync(LocationCreateDto locationCreateDto);
        Task<LocationDto> GetCurrentLocationAsync(string vehicleId);
        Task<List<LocationDto>> GetLocationListAsync(string vehicleId, DateTime fromDateTime, DateTime toDateTime);
    }
}
