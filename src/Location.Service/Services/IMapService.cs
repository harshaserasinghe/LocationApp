using Location.Service.Dtos.Google;
using System.Threading.Tasks;

namespace Location.Service.Services
{
    public interface IMapService
    {
        Task<MapDto> GetMapDataAsync(double latitude, double longitude);
    }
}