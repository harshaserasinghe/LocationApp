using Location.Service.Dtos.Google;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Location.Service.Services
{
    public class MapService : IMapService
    {
        private string apiKey = "AIzaSyD-pJPalKTwQU6upHjAATDdvj4OO5aKwag";
        public async Task<MapDto> GetMapDataAsync(double latitude, double longitude)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var url = $"https://maps.googleapis.com/maps/api/geocode/json?latlng={latitude},{longitude}&key={apiKey}";
                    var response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var mapDto = JsonConvert.DeserializeObject<MapDto>(responseBody);

                    var locality = string.Empty;
                    mapDto.Results.ForEach(result =>
                    {
                        result.AddressComponents.ForEach(address =>
                        {
                            if (address.Types.Any(type => type.ToLower() == "locality"))
                            {
                                locality = address.LongName;
                            }
                        });
                    });

                    var val = mapDto.Results.Select(r => r.AddressComponents
                    .FirstOrDefault(a => a.LongName == "Pannipitiya"));

                    return mapDto;
                }
            }
            catch (HttpRequestException e)
            {
                return null;
            }
        }
    }
}
