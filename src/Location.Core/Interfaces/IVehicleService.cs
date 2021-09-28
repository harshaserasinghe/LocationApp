using Location.Core.Dtos;
using System.Threading.Tasks;

namespace Location.Core.Interfaces
{
    public interface IVehicleService
    {
        Task<VehicleDto> GetVehicleAsync(string vehcileId);
        Task AddVehicleAsync(VehicleCreateDto VehicleCreateDto);
        Task<bool> IsRegisteredAsync(string vehcileId);
    }
}
