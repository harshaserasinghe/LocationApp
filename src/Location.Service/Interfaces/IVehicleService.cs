using Location.Service.Dtos;
using System.Threading.Tasks;

namespace Location.Service.Interfaces
{
    public interface IVehicleService
    {
        Task<VehicleDto> GetVehicleAsync(string vehcileId);
        Task AddVehicleAsync(VehicleCreateDto VehicleCreateDto);
        Task<bool> IsRegisteredAsync(string vehcileId);
    }
}
