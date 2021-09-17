using Location.Common.Settings;
using Location.Service.Dtos.Google;
using Location.Service.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Location.Service.Services
{
    public class MapService : IMapService
    {
        private readonly GoogleMapConfig googleMapConfig;
        private readonly ILogger<MapService> logger;
        public MapService(IOptions<GoogleMapConfig> googleMapConfig, ILogger<MapService> logger)
        {
            this.googleMapConfig = googleMapConfig.Value;
            this.logger = logger;
        }

        public async Task<MapDto> GetMapDataAsync(double latitude, double longitude)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var endPoint = $"{googleMapConfig.ApiEndpoint}?latlng={latitude},{longitude}&key={googleMapConfig.ApiKey}";
                    var response = await client.GetAsync(endPoint);
                    response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var mapDto = JsonConvert.DeserializeObject<MapDto>(responseBody);
                    return mapDto;
                }
            }
            catch (Exception exception)
            {
                logger.LogError(exception.Message, exception);
                return null;
            }
        }

        public async Task<string> GetLocality(double latitude, double longitude)
        {
            var mapDto = await GetMapDataAsync(latitude, longitude);

            if (mapDto is null || mapDto.Status.ToLower() != "ok")      
                return string.Empty;          

            var locality = mapDto.Results.FirstOrDefault()
                .AddressComponents.FirstOrDefault(address => address.Types.Any(addrType => addrType.ToLower() == "locality"))?.LongName;

            return locality;
        }

    }
}
