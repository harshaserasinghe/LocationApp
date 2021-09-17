using Newtonsoft.Json;
using System.Collections.Generic;

namespace Location.Service.Dtos.Google
{
    public class MapDto
    {
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "results")]
        public List<Result> Results { get; set; } = new List<Result>();
    }
}
