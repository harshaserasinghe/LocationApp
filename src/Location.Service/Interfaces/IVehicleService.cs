using Location.Service.Dtos;
using Location.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Location.Service.Interfaces
{
    public interface IVehicleService
    {
        Task<VehicleDto> GetVehicleAsync(string vehcileId);
        Task AddVehicleAsync(VehicleCreateDto vehicleDto);
        Task<bool> IsRegisteredAsync(string vehcileId);
    }
}
