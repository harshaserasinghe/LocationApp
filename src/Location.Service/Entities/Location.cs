using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Location.Service.Entities
{
    public class Location
    {
        public Location(string vehicleId, double latitude, double longitude, DateTime createdDate)
        {
            Id = vehicleId;
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

        public void SetNewId() => this.Id = Guid.NewGuid().ToString();
    }
}
