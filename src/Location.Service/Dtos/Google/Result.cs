﻿using Newtonsoft.Json;
using System.Collections.Generic;

namespace Location.Service.Dtos.Google
{
    public class Result
    {
        [JsonProperty(PropertyName = "address_components")]
        public List<AddressComponent> AddressComponents { get; set; } = new List<AddressComponent>();
    }
}