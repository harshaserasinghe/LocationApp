using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Location.Service.Entities
{
    public class Vehicle
    {
        public Vehicle(string vehicleId, string licenceNo)
        {
            Id = vehicleId;
            VehicleId = vehicleId;
            LicenceNo = licenceNo; 
            CreatedDate = DateTime.UtcNow;
        }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public string VehicleId { get; set; }
        public string LicenceNo { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
