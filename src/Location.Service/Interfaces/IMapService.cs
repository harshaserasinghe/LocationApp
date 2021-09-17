using Location.Service.Dtos.Google;
using System.Threading.Tasks;

namespace Location.Service.Interfaces
{
    public interface IMapService
    {
        Task<MapDto> GetMapDataAsync(double latitude, double longitude);
        Task<string> GetLocality(double latitude, double longitude);
    }
}