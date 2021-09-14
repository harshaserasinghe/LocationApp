using Location.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Location.Service.Interfaces
{
    public interface ILocationService
    {
        Task AddLocationAsync(LocationCreateDto locationDto);
        Task<Entities.Location> GetCurrentLocationAsync(string vehicleId);
        Task<List<Entities.Location>> GetLocationListAsync(string vehicleId, DateTime fromDateTime, DateTime toDateTime);
    }
}
