using Newtonsoft.Json;
using System;

namespace Location.Core.Entities
{
    public class Location
    {
        public Location(string vehicleId, double latitude, double longitude, DateTime createdDate)
        {
            Id = Guid.NewGuid().ToString();
            VehicleId = vehicleId;
            Latitude = latitude;
            Longitude = longitude;
            CreatedDate = createdDate.ToUniversalTime();
        }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public string VehicleId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime CreatedDate { get; set; }

        public void UpdateId() => Id = VehicleId;
    }
}
