using Newtonsoft.Json;
using System.Collections.Generic;

namespace Location.Service.Dtos.Google
{
    public class AddressComponent
    {
        [JsonProperty(PropertyName = "types")]
        public List<string> Types { get; set; } = new List<string>();

        [JsonProperty(PropertyName = "short_name")]
        public string ShortName { get; set; }

        [JsonProperty(PropertyName = "long_name")]
        public string LongName { get; set; }
    }
}